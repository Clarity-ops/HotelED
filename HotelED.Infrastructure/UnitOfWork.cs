using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;

namespace HotelED.Infrastructure;

public class UnitOfWork(ApplicationDbContext ctx) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await ctx.SaveChangesAsync();
    }
}