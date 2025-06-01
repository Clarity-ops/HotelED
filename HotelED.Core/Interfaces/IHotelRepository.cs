using System.Runtime.InteropServices;
using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IHotelRepository
{
    // CRUD для сутності Hotel
    Task<Hotel?> GetByIdAsync(Guid hotelId);
    Task<IEnumerable<Hotel>> GetAllByOwnerAsync(Guid ownerId);
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid hotelId);

    // Додаткові методи
    Task<bool> ExistsAsync(Guid hotelId);
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<IEnumerable<Hotel>> GetHotels(int count);
    Task<bool> ExistsByNameAsync(string name, Guid ownerId);
    Task<Hotel?> GetWithImagesAsync(Guid hotelId);
    Task<Hotel?> GetWithImagesAsync(string name, Guid ownerId);
}