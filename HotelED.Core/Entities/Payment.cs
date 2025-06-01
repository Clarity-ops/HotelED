using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public decimal Amount { get; set; }
    [MaxLength(40)] public string Currency { get; set; } = "dollar";
    [MaxLength(30)]
    public required string Status { get; set; }
    public DateTime PaidAt { get; set; } = DateTime.UtcNow;

    public Booking Booking { get; set; } = null!;
}