using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.Repositories;

public class BookingRepository(ApplicationDbContext ctx) : IBookingRepository
{
    public async Task AddAsync(Booking booking)
    {
        await ctx.Bookings.AddAsync(booking);
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await ctx.Bookings
            .Include(b => b.Hotel)
            .Include(b => b.Payment)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> GetByUserAsync(Guid userId)
    {
        return await ctx.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Hotel)
            .Include(b => b.Payment)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateAsync(Booking booking)
    {
        ctx.Bookings.Update(booking);
        return Task.CompletedTask;
    }
}