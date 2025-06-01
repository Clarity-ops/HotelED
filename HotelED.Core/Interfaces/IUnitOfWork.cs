namespace HotelED.Core.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}