using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetByBookingIdAsync(Guid bookingId);
    Task UpdateAsync(Payment payment);
}