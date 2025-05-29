using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.Entities;

public class Image
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    [MaxLength(150)]
    public required string Url { get; set; }
    [MaxLength(50)]
    public required string AltText { get; set; }
    public int SortOrder { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public required Hotel Hotel { get; set; }
}