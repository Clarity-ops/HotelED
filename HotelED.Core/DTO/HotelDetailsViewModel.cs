using HotelED.Core.Entities;

namespace HotelED.Core.DTO;

public class HotelDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    // Інші поля (опис, ціна, картинки тощо)
    public string Description { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public IEnumerable<Image> Images { get; set; } = new List<Image>();

    // Відгуки та середній рейтинг
    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
    public double AverageRating { get; set; }
}