using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.DTO;

public class BookingCreateViewModel
{
    [Required]
    public Guid HotelId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CheckIn { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CheckOut { get; set; }
}