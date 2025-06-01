using HotelED.Core.Entities;

namespace HotelED.Core.Interfaces;

public interface IReviewRepository
{
    Task AddAsync(Review review);
    Task<IEnumerable<Review>> GetByHotelAsync(Guid hotelId);
    Task<double> GetAverageRatingAsync(Guid hotelId);
    Task<IEnumerable<Review>> GetUsersReview(Guid userId);
}