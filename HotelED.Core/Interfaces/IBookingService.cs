using HotelED.Core.DTO;
using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IBookingService
{
    /// <summary>
    /// Бронює готель: запис у Booking, створює Payment (симуляція).
    /// </summary>
    Task<Result> CreateBookingAsync(BookingCreateViewModel vm, Guid userId);

    /// <summary>
    /// Повертає бронювання конкретного користувача
    /// </summary>
    Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId);
}