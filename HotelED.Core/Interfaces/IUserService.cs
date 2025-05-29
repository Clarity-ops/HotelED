using HotelED.Core.DTO;

namespace HotelED.Core.Interfaces;

public interface IUserService
{
    public Task<UserProfileDto?> GetProfileAsync(Guid userId);
    public Task<UserProfileDto?> GetProfileAsync(string email);
}