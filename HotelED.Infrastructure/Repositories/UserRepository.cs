using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext ctx) : IUserRepository
{
    // 1) Отримати базову сутність користувача
    public async Task<ApplicationUser?> GetByIdAsync(Guid userId)
    {
        return await ctx.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    // 2) Отримати користувача разом з усіма навігаційними даними
    public async Task<ApplicationUser?> GetWithDetailsAsync(Guid userId)
    {
        return await ctx.Users
            .Include(u => u.Bookings)
            .ThenInclude(b => b.Hotel)
            .Include(u => u.Bookings)
            .ThenInclude(b => b.Payment)
            .Include(u => u.Reviews)
            .ThenInclude(r => r.Hotel)
            .Include(u => u.Hotels)       // якщо користувач створює готелі
            .ThenInclude(h => h.Images)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<ApplicationUser?> GetWithDetailsAsync(string email)
    {
        return await ctx.Users
            .Include(u => u.Bookings)
            .ThenInclude(b => b.Hotel)
            .Include(u => u.Bookings)
            .ThenInclude(b => b.Payment)
            .Include(u => u.Reviews)
            .ThenInclude(r => r.Hotel)
            .Include(u => u.Hotels)       // якщо користувач створює готелі
            .ThenInclude(h => h.Images)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    // 3) Отримати всіх користувачів (наприклад, для адмінки)
    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        return await ctx.Users
            .AsNoTracking()
            .ToListAsync();
    }
}