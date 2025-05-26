namespace HotelED.Core.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Image> Images { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}