namespace HotelED.Core.Interfaces;

/// <summary>
/// Симуляція платіжного шлюзу
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Імітує платіж:
    /// повертає true (оплата успішна) або false (помилка)
    /// </summary>
    Task<bool> PayAsync(decimal amount);
}