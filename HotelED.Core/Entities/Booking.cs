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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Nights { get; set; } // автоматично обчислюється
    public decimal TotalPrice { get; set; } // Nights * Hotel.PricePerDay
    public ApplicationUser User { get; set; } = null!;
    public Hotel Hotel { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
}