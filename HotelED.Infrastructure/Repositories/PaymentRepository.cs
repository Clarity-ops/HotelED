using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.Repositories;

public class PaymentRepository(ApplicationDbContext ctx) : IPaymentRepository
{
    public async Task AddAsync(Payment payment)
    {
        await ctx.Payments.AddAsync(payment);
    }

    public async Task<Payment?> GetByBookingIdAsync(Guid bookingId)
    {
        return await ctx.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
    }

    public Task UpdateAsync(Payment payment)
    {
        ctx.Payments.Update(payment);
        return Task.CompletedTask;
    }
}