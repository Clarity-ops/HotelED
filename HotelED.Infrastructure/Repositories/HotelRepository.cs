using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.Repositories;

public class HotelRepository(ApplicationDbContext ctx) : IHotelRepository
{
    public async Task<Hotel?> GetByIdAsync(Guid hotelId)
    {
        return await ctx.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == hotelId);
    }

    public async Task<IEnumerable<Hotel>> GetAllByOwnerAsync(Guid ownerId)
    {
        return await ctx.Hotels
            .Where(h => h.OwnerId == ownerId)
            .Include(h => h.Images)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Hotel hotel)
    {
        await ctx.Hotels.AddAsync(hotel);
    }

    public Task UpdateAsync(Hotel hotel)
    {
        ctx.Hotels.Update(hotel);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid hotelId)
    {
        var searched = await ctx.Hotels.FindAsync(hotelId);
        if (searched != null) ctx.Hotels.Remove(searched);
    }

    public async Task<bool> ExistsAsync(Guid hotelId)
    {
        return await ctx.Hotels.AnyAsync(hotel => hotel.Id == hotelId);
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid ownerId)
    {
        return await ctx.Hotels.AnyAsync(hotel => hotel.Name == name && hotel.OwnerId == ownerId);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await ctx.Hotels.AnyAsync(hotel => hotel.Name == name);
    }

    public async Task<Hotel?> GetWithImagesAsync(Guid hotelId)
    {
        return await ctx.Hotels
            .Include(h => h.Images)
            .FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<Hotel?> GetWithImagesAsync(string name, Guid ownerId)
    {
        return await ctx.Hotels
            .Include(h => h.Images)
            .FirstOrDefaultAsync(h => h.Name == name && h.OwnerId == ownerId);
    }

    public Task<IEnumerable<Hotel>> GetAllHotels()
    {
        return Task.FromResult(ctx.Hotels.Include(hotel => hotel.Images)
            .Include(hotel => hotel.Reviews).AsNoTracking().AsEnumerable());
    }

    public Task<IEnumerable<Hotel>> GetHotels(int count)
    {
        return Task.FromResult<IEnumerable<Hotel>>(ctx.Hotels
            .OrderBy(hotel => hotel.Reviews.Count)
            .Include(hotel => hotel.Images)
            .Include(hotel => hotel.Reviews)
            .AsNoTracking()
            .Take(count));
    }
}