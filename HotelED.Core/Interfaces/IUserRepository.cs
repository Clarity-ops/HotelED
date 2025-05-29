using HotelED.Core.Entities.Identities;

namespace HotelED.Core.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(Guid userId);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetWithDetailsAsync(Guid userId);
    Task<ApplicationUser?> GetWithDetailsAsync(string email);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
}