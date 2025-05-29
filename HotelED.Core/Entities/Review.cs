using HotelED.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.Entities;

public class Review
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public required Guid UserId { get; set; }
    [Range(1, 10, ErrorMessage = "Рейтинг повинен бути від 1 до 10.")]
    public int Rating { get; set; }    // 1–10
    [MaxLength(200)]
    public required string Comment { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public required Hotel Hotel { get; set; }
    public required ApplicationUser User { get; set; }
}