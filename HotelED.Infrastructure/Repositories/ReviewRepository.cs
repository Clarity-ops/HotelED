using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.Repositories;

public class ReviewRepository(ApplicationDbContext ctx) : IReviewRepository
{
    public async Task AddAsync(Review review)
    {
        await ctx.Reviews.AddAsync(review);
    }

    public async Task<IEnumerable<Review>> GetByHotelAsync(Guid hotelId)
    {
        return await ctx.Reviews
            .Where(r => r.HotelId == hotelId)
            .Include(r => r.User) // якщо треба вивести ім’я користувача
            .OrderByDescending(r => r.Date)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingAsync(Guid hotelId)
    {
        var ratings = await ctx.Reviews
            .Where(r => r.HotelId == hotelId)
            .Select(r => (int?)r.Rating)
            .ToListAsync();
        if (ratings == null || !ratings.Any(r => r.HasValue))
            return 0.0;
        return Math.Round(ratings.Average(r => r.Value), 1);
    }

    public async Task<IEnumerable<Review>> GetUsersReview(Guid userId)
    {
        return await ctx.Reviews.Where(review => review.UserId == userId).ToListAsync();
    }
}