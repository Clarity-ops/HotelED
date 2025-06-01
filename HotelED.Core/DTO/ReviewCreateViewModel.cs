using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.DTO;

public class ReviewCreateViewModel
{
    [Required]
    public Guid HotelId { get; set; }

    [Required]
    [Range(1, 10)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;
}