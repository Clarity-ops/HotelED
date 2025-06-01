using HotelED.Core.Interfaces;

namespace HotelED.Core.Services;

/// <summary>
/// Симуляція платіжного шлюзу: випадково успіх або неуспіх.
/// </summary>
public class PaymentService : IPaymentService
{
    private static readonly Random Rand = new();

    public Task<bool> PayAsync(decimal amount)
    {
        // Наприклад, 80% шанс, що оплата пройде
        var success = Rand.NextDouble() < 0.8;
        return Task.FromResult(success);
    }
}