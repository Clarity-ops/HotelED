using HotelED.Core.DTO;
using HotelED.Core.Entities;
using HotelED.Core.Interfaces;

namespace HotelED.Core.Services;

public class ReviewService(
    IReviewRepository reviewRepo,
    IHotelRepository hotelRepo,
    IUnitOfWork unitOfWork)
    : IReviewService
{
    public async Task<Result> CreateReviewAsync(ReviewCreateViewModel vm, Guid userId)
        {
            // 1. Перевіряємо: чи існує готель
            var hotel = await hotelRepo.GetByIdAsync(vm.HotelId);
            if (hotel == null)
                return Result.Fail("Готель не знайдено.");

            // 2. Перевірка, чи користувач вже залишав відгук (опційно)
            // Якщо потрібно обмежити один відгук від користувача на один готель:
            var existing = (await reviewRepo.GetByHotelAsync(vm.HotelId))
                           .FirstOrDefault(r => r.UserId == userId);
            if (existing != null)
                return Result.Fail("Ви вже залишали відгук для цього готелю.");

            // 3. Створюємо новий Review
            var review = new Review
            {
                Id = Guid.NewGuid(),
                HotelId = vm.HotelId,
                UserId = userId,
                Rating = vm.Rating,
                Comment = vm.Comment,
                Date = DateTime.UtcNow
            };
            await reviewRepo.AddAsync(review);

            // 4. Зберігаємо зміни
            await unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<IEnumerable<Review>> GetHotelReviewsAsync(Guid hotelId)
        {
            // просто делегуємо репозиторію
            return await reviewRepo.GetByHotelAsync(hotelId);
        }

        public async Task<double> GetHotelAverageRatingAsync(Guid hotelId)
        {
            return await reviewRepo.GetAverageRatingAsync(hotelId);
        }

        public async Task<IEnumerable<Review>> GetUsersReviews(Guid userId)
        {
            return await reviewRepo.GetUsersReview(userId);
        }
}