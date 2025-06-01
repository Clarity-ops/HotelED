using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;

namespace HotelED.Infrastructure.Repositories;

public class ImageRepository(ApplicationDbContext ctx) : IImageRepository
{
    public async Task AddAsync(Image image)
    {
        await ctx.Images.AddAsync(image);
    }

    public Task DeleteAsync(Image image)
    {
        ctx.Images.Remove(image);
        return Task.CompletedTask;
    }
}