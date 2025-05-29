using System.ComponentModel.DataAnnotations;
using HotelED.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Core.DTO;

public class RegisterForm : IValidatableObject
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Remote("IsEmailExists", "Auth", HttpMethod = "GET", ErrorMessage = "User with this email already exists")]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var obj = validationContext.ObjectInstance as RegisterForm ?? throw new ArgumentNullException();
        var isValid = true;
        if (!obj.Password.Any(char.IsAsciiDigit))
        {
            isValid = false;
            yield return new ValidationResult("Password must contain a digit");
        }

        if (!obj.Password.Any(char.IsUpper))
        {
            isValid = false;
            yield return new ValidationResult("Password must contain an upper letter");
        }
        if (isValid)
            yield return ValidationResult.Success!;
    }
}