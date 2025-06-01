using System.Security.Claims;
using HotelED.Core.DTO;
using HotelED.Core.Entities;
using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using HotelED.Core.Services;
using HotelED.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers
{
    [Route("[controller]/[action]")]
    public class HotelController(
        IHotelService hotelService,
        UserManager<ApplicationUser> userManager,
        IReviewService reviewService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(HotelCreateInfo createInfo)
        {
            if (!ModelState.IsValid || createInfo.Photos.Count == 0)
            {
                ViewBag.Error = ModelState.Values.SelectMany(errs => errs.Errors).Select(e => e.ErrorMessage);
                return View();
            }

            var ownerId = Guid.Parse(userManager.GetUserId(User) ?? throw new InvalidOperationException());
            var result = await hotelService.CreateHotelAsync(createInfo, ownerId);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(createInfo);
            }

            return RedirectToAction("Hotels", "User");
        }

        [HttpGet]
        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [HttpGet("{id:guid}/{slug?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id, string? slug = null)
        {
            var hotel = await hotelService.GetHotelDetailsAsync(id);
            if (hotel == null) return NotFound();

            // Отримуємо відгуки та середній рейтинг
            var reviews = await reviewService.GetHotelReviewsAsync(id);
            var avgRating = await reviewService.GetHotelAverageRatingAsync(id);

            var vm = new HotelDetailsViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                Description = hotel.Description,
                PricePerDay = hotel.PricePerDay,
                Images = hotel.Images,
                Reviews = reviews,
                AverageRating = avgRating
            };

            return View(vm);
        }
    }
}