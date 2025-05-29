using System.ComponentModel.DataAnnotations;
using HotelED.Core.Entities.Identities;

namespace HotelED.Core.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public Guid HotelId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    [MaxLength(30)]
    public required string Status { get; set; }

    public required ApplicationUser User { get; set; }
    public required Hotel Hotel { get; set; }
    public required Payment Payment { get; set; }
}