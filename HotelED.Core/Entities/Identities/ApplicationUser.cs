using Microsoft.AspNetCore.Identity;

namespace HotelED.Core.Entities.Identities;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навігаційні властивості
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}