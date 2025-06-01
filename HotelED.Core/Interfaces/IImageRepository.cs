using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IImageRepository
{
    Task AddAsync(Image image);
    Task DeleteAsync(Image image);
    // Можна додати методи GetByHotelIdAsync тощо, за потреби
}