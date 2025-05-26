using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.DTO;

public class LoginForm
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required string Password { get; set; }
}