using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.DTO;

public class HotelCreateInfo
{
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }
    [Required]
    [MaxLength(60)]
    public required string Location { get; set; }
    [MaxLength(300)]
    [Required]
    public required string Description { get; set; }
    [MaxLength(20)]
    [Required]
    public required string Category { get; set; }
    [Required]
    public List<IFormFile> Photos { get; set; } = [];
    [Required]
    [Range(0, double.MaxValue)]
    public decimal PricePerDay { get; set; }
}

