using HotelED.Core.DTO;
using HotelED.Core.Entities;
using HotelED.Core.Interfaces;

namespace HotelED.Core.Services;

public class HotelService(IHotelRepository repo, IUnitOfWork unit, IImageRepository imageRepo, IFileStorageService fileStorage) : IHotelService
{
    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
    {
        return await repo.GetAllHotels();
    }

    public async Task<IEnumerable<Hotel>> GetHotelsAsync(int count)
    {
        return await repo.GetHotels(count);
    }

    public async Task<IEnumerable<Hotel>> GetHotelsByOwnerAsync(Guid ownerId)
    {
        return await repo.GetAllByOwnerAsync(ownerId);
    }

    public async Task<Hotel?> GetHotelDetailsAsync(Guid hotelId, Guid ownerId)
    {
        var hotel = await repo.GetWithImagesAsync(hotelId);
        if (hotel == null || hotel.OwnerId != ownerId)
            return null;
        return hotel;
    }

    public async Task<Hotel?> GetHotelDetailsAsync(Guid hotelId)
    {
        var hotel = await repo.GetWithImagesAsync(hotelId);
        return hotel ?? null;
    }

    private static readonly string[] ValidFormats = [".jpg", ".jpeg", ".png", ".gif"];

    public async Task<Result> CreateHotelAsync(HotelCreateInfo vm, Guid ownerId)
    {
        var exists = await repo.ExistsByNameAsync(vm.Name, ownerId);

        if (exists)
            return Result.Fail("Готель з такою назвою вже існує");

        var hotel = new Hotel
        {
            Id          = Guid.NewGuid(),
            Name        = vm.Name,
            Location    = vm.Location,
            Category    = vm.Category,
            Description = vm.Description,
            OwnerId     = ownerId,
            CreatedAt   = DateTime.UtcNow,
            PricePerDay = vm.PricePerDay
        };

        await repo.AddAsync(hotel);

        if (vm.Photos.Count != 0)
        {
            // Створюємо підпапку, наприклад: "hotels/{hotel.Id}"
            var subfolder = Path.Combine("hotels", hotel.Id.ToString());

            var sortOrder = 0;
            foreach (var formFile in vm.Photos)
            {
                if (formFile.Length <= 0) 
                    continue;

                // Валідний формат?
                var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                if (!ValidFormats.Contains(ext))
                    return Result.Fail("Непідтримуваний формат зображення.");

                // Зберігаємо файл, отримуємо URL
                string fileUrl;
                try
                {
                    fileUrl = await fileStorage.SaveFileAsync(formFile, subfolder);
                }
                catch (Exception ex)
                {
                    return Result.Fail($"Помилка при збереженні файлу: {ex.Message}");
                }

                // Формуємо ентіті Image
                var img = new Image
                {
                    Id         = Guid.NewGuid(),
                    HotelId    = hotel.Id,
                    Url        = fileUrl,
                    AltText    = hotel.Name,
                    SortOrder  = sortOrder++,
                    UploadedAt = DateTime.UtcNow
                };
                await imageRepo.AddAsync(img);
            }
        }

        // 5) Зберігаємо усі зміни одним SaveChangesAsync
        await unit.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateHotelAsync(HotelCreateInfo vm, Guid ownerId)
    {
        var hotel = await repo.GetWithImagesAsync(vm.Name, ownerId);
        if (hotel == null)
            return Result.Fail("Готель не знайдено або у вас немає прав.");

        hotel.Name = vm.Name;
        hotel.Location = vm.Location;
        hotel.Category = vm.Category;
        hotel.Description = vm.Description;
        
        await repo.UpdateAsync(hotel);
        var deletedPhotos = new List<string>();
        if (vm.Photos.Count != 0)
        {
            foreach (var imgId in vm.Photos)
            {
                while (true)
                {
                    var toRemove = hotel.Images.FirstOrDefault(i => Path.GetFileName(i.Url) != imgId.FileName);
                    if (toRemove == null) break;
                    try
                    {
                        await fileStorage.DeleteFileAsync(toRemove.Url);
                    }
                    catch
                    {
                        // Логіку помилки можна розширити, але не зупиняти всю операцію
                    }
                    deletedPhotos.Add(imgId.FileName);
                    await imageRepo.DeleteAsync(toRemove);
                }
            }
        }

        // 4) Додаємо нові фото (як у Create)
        if (vm.Photos.Count != 0)
        {
            var subfolder = Path.Combine("hotels", hotel.Id.ToString());
            var sortOrder = hotel.Images.Count > 0 ? hotel.Images.Max(i => i.SortOrder) + 1 : 0;

            foreach (var formFile in vm.Photos.ExceptBy(deletedPhotos, file => file.FileName))
            {
                if (formFile.Length <= 0) 
                    continue;

                var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                if (!ValidFormats.Contains(ext))
                    return Result.Fail("Непідтримуваний формат зображення.");

                string fileUrl;
                try
                {
                    fileUrl = await fileStorage.SaveFileAsync(formFile, subfolder);
                }
                catch (Exception ex)
                {
                    return Result.Fail($"Помилка при збереженні файлу: {ex.Message}");
                }

                var img = new Image
                {
                    Id         = Guid.NewGuid(),
                    HotelId    = hotel.Id,
                    Url        = fileUrl,
                    AltText    = hotel.Name,
                    SortOrder  = sortOrder++,
                    UploadedAt = DateTime.UtcNow
                };
                await imageRepo.AddAsync(img);
            }
        }

        // 5) Save all changes (Hotel update, Images delete/add)
        await unit.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> DeleteHotelAsync(Guid hotelId, Guid ownerId)
    {
        var hotel = await repo.GetWithImagesAsync(hotelId);
        if (hotel == null || hotel.OwnerId != ownerId)
            return Result.Fail("Готель не знайдено або у вас немає прав.");

        // 1) Видаляємо файли всіх фото
        foreach (var img in hotel.Images)
        {
            try
            {
                await fileStorage.DeleteFileAsync(img.Url);
            }
            catch
            {
                // Якщо помилка – продовжуємо. Пізніше можна логувати.
            }
            await imageRepo.DeleteAsync(img);
        }

        // 2) Видаляємо сам готель
        await repo.DeleteAsync(hotel.Id);

        // 3) Зберігаємо зміни
        await unit.SaveChangesAsync();
        return Result.Ok();
    }
}