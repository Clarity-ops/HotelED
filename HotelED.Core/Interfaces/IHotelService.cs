using HotelED.Core.DTO;
using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;
public interface IHotelService
{
    Task<IEnumerable<Hotel>> GetAllHotelsAsync();
    Task<IEnumerable<Hotel>> GetHotelsAsync(int count);
    Task<IEnumerable<Hotel>> GetHotelsByOwnerAsync(Guid ownerId);
    Task<Hotel?> GetHotelDetailsAsync(Guid hotelId, Guid ownerId);
    Task<Hotel?> GetHotelDetailsAsync(Guid hotelId);
    Task<Result> CreateHotelAsync(HotelCreateInfo vm, Guid ownerId);
    Task<Result> UpdateHotelAsync(HotelCreateInfo vm, Guid ownerId);
    Task<Result> DeleteHotelAsync(Guid hotelId, Guid ownerId);
}