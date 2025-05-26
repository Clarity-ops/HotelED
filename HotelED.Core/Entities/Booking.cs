using HotelED.Core.Entities.Identities;

namespace HotelED.Core.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public Guid HotelId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public required string Status { get; set; }

    public required ApplicationUser User { get; set; }
    public required Hotel Hotel { get; set; }
    public required Payment Payment { get; set; }
}