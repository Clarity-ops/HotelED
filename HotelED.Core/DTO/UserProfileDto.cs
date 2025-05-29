using HotelED.Core.Entities;

namespace HotelED.Core.DTO;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Hotel>? Hotels { get; set; }
}