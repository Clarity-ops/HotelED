using HotelED.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HotelED.Core.Services;

/// <summary>
    /// Проста реалізація, яка зберігає файли у wwwroot/images/{subfolder}.
    /// </summary>
    public class FileStorageService(IWebHostEnvironment env) : IFileStorageService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            // subfolder, наприклад: "hotels/{hotelId}"
            var rootFolder = Path.Combine(env.WebRootPath, "images", subfolder);
            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var uniqueName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(rootFolder, uniqueName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Повертаємо відносний URL, який браузер може використовувати
            // наприклад: "/images/hotels/{hotelId}/{uniqueName}"
            var relativePath = Path.Combine("images", subfolder, uniqueName)
                                   .Replace("\\", "/");
            return "/" + relativePath;
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            // fileUrl, наприклад: "/images/hotels/{hotelId}/{uniqueName}"
            if (string.IsNullOrWhiteSpace(fileUrl))
                return Task.CompletedTask;

            var wwwroot = env.WebRootPath.TrimEnd(Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(wwwroot, fileUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return Task.CompletedTask;
        }
    }