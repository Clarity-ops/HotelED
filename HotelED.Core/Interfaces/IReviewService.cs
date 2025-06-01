using HotelED.Core.DTO;
using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;
public interface IReviewService
{
    /// <summary>
    /// Додає відгук до бази (перевіряє коректність, авторизацію тощо).
    /// </summary>
    Task<Result> CreateReviewAsync(ReviewCreateViewModel vm, Guid userId);

    /// <summary>
    /// Повертає всі відгуки для готелю.
    /// </summary>
    Task<IEnumerable<Review>> GetHotelReviewsAsync(Guid hotelId);

    /// <summary>
    /// Обчислює середню оцінку для готелю.
    /// </summary>
    Task<double> GetHotelAverageRatingAsync(Guid hotelId);

    Task<IEnumerable<Review>> GetUsersReviews(Guid userId);
}