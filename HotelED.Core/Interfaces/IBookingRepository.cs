using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetByUserAsync(Guid userId);
    Task AddAsync(Booking booking);

    Task UpdateAsync(Booking booking);
    // Ми не передбачаємо видалення бронювання в даній версії
}