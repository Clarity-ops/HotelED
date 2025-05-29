using HotelED.Core.DTO;
using HotelED.Core.Interfaces;

namespace HotelED.Core.Services;

public class UserService(IUserRepository users) : IUserService
{
    public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
    {
        var user = await users.GetWithDetailsAsync(userId);
        if (user == null) return null;

        return new UserProfileDto {
            Id        = user.Id,
            Name      = user.Name,
            Email     = user.Email ?? "",
            Bookings  = user.Bookings,
            Reviews   = user.Reviews,
            Hotels    = user.Hotels
        };
    }

    public async Task<UserProfileDto?> GetProfileAsync(string email)
    {
        var user = await users.GetByEmailAsync(email);
        if (user == null) return null;

        return new UserProfileDto {
            Id        = user.Id,
            Name      = user.Name,
            Email     = user.Email ?? "",
            Bookings  = user.Bookings,
            Reviews   = user.Reviews,
            Hotels    = user.Hotels
        }; 
    }
}