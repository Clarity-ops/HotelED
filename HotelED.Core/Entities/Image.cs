namespace HotelED.Core.Entities;

public class Image
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public required string Url { get; set; }
    public required string AltText { get; set; }
    public int SortOrder { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public required Hotel Hotel { get; set; }
}