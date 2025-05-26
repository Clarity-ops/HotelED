using System.ComponentModel.DataAnnotations;

namespace HotelED.Core.DTO;

public class RegisterForm
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
}