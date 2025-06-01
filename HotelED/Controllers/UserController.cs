using HotelED.Core.DTO;
using HotelED.Core.Entities;
using HotelED.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers;
[Route("[controller]/[action]")]
public class UserController(IUserService userService, IHotelService hotelService, IBookingService bookingService, IReviewService reviewService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name == null) return RedirectToAction("Login", "Auth");

        var profile = await userService.GetProfileAsync(User.Identity.Name);
        if (profile is null) return RedirectToAction("Login", "Auth");
        var bookings = await bookingService.GetUserBookingsAsync(profile.Id);
        var reviews = await reviewService.GetUsersReviews(profile.Id);
        var vm = new UserProfileDto { Bookings = bookings as ICollection<Booking>, Email = profile.Email, Name = profile.Name, Reviews = reviews as ICollection<Review> };
        return View(vm);
    }

    public async Task<IActionResult> Bookings()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name == null) return RedirectToAction("Login", "Auth");
        var profile = await userService.GetProfileAsync(User.Identity.Name);
        if (profile is null) return RedirectToAction("Login", "Auth");
        var bookings = await bookingService.GetUserBookingsAsync(profile.Id);
        return View(bookings);
    }

    public async Task<IActionResult> Hotels()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name == null) return RedirectToAction("Login", "Auth");
        var profile = await userService.GetProfileAsync(User.Identity.Name);
        if (profile is null) return RedirectToAction("Login", "Auth");
        var hotels = await hotelService.GetHotelsByOwnerAsync(profile.Id);
        

        return View(hotels);
    }
}