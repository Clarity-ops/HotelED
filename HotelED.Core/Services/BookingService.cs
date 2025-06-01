using HotelED.Core.DTO;
using HotelED.Core.Entities;
using HotelED.Core.Interfaces;

namespace HotelED.Core.Services;

/// <summary>
    /// Бізнес-логіка бронювання: розрахунок кількості діб, суми, виклик оплати, збереження записів.
    /// </summary>
    public class BookingService(
        IBookingRepository bookingRepo,
        IHotelRepository hotelRepo,
        IPaymentService paymentSvc,
        IPaymentRepository paymentRepo,
        IUnitOfWork unitOfWork)
        : IBookingService
    {
        public async Task<Result> CreateBookingAsync(BookingCreateViewModel vm, Guid userId)
        {
            // 1. Перевірка дат
            if (vm.CheckIn.Date >= vm.CheckOut.Date)
                return Result.Fail("Дата виїзду має бути пізніше дати заїзду.");

            // 2. Завантажуємо готель
            var hotel = await hotelRepo.GetByIdAsync(vm.HotelId);
            if (hotel == null)
                return Result.Fail("Готель не знайдено.");

            // 3. Обчислюємо кількість ночей
            var nights = (int)(vm.CheckOut.Date - vm.CheckIn.Date).TotalDays;
            if (nights <= 0)
                return Result.Fail("Кількість ночей має бути щонайменше 1.");

            // 4. Розрахунок суми
            var totalPrice = nights * hotel.PricePerDay;

            // 5. Створюємо Booking-ентіті
            var booking = new Booking
            {
                Id         = Guid.NewGuid(),
                UserId     = userId,
                HotelId    = hotel.Id,
                CheckIn    = DateTime.SpecifyKind(vm.CheckIn.Date, DateTimeKind.Utc),
                CheckOut   = DateTime.SpecifyKind(vm.CheckOut.Date, DateTimeKind.Utc),
                Nights     = nights,
                TotalPrice = totalPrice,
                Status     = "Pending", // початковий статус
                CreatedAt  = DateTime.UtcNow
            };
            await bookingRepo.AddAsync(booking);
            await unitOfWork.SaveChangesAsync();
            // 6. Імітація оплати
            bool paymentOk;
            try
            {
                paymentOk = await paymentSvc.PayAsync(totalPrice);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Помилка при ініціалізації оплати: {ex.Message}");
            }

            // 7. Створюємо запис Payment
            var payment = new Payment
            {
                Id         = Guid.NewGuid(),
                BookingId  = booking.Id,
                Amount     = totalPrice,
                Status     = paymentOk ? "Paid" : "Failed",
                PaidAt     = DateTime.UtcNow
            };
            await paymentRepo.AddAsync(payment);

            // 8. Оновлюємо статус бронювання
            booking.Status = paymentOk ? "Confirmed" : "Failed";
            await bookingRepo.UpdateAsync(booking);

            // 9. Зберігаємо усі зміни
            await unitOfWork.SaveChangesAsync();

            if (!paymentOk)
                return Result.Fail("Оплата не пройшла. Спробуйте пізніше.");

            return Result.Ok();
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(Guid userId)
        {
            return await bookingRepo.GetByUserAsync(userId);
        }
    }