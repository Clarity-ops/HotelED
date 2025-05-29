namespace HotelED.Core.Models;

public class Email(string email)
{
    public string GetEmail { get; } = email.ToLower();
}