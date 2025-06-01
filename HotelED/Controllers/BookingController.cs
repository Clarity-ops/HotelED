using HotelED.Core.DTO;
using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers
{
    [Route("[controller]/[action]")]
    public class BookingController(IBookingService bookingService, UserManager<ApplicationUser> userManager) : Controller
    {
        // GET: /Booking/Create?hotelId={hotelId}
        [HttpGet("{hotelId:guid}")]
        public IActionResult Create(Guid hotelId)
        {
            var vm = new BookingCreateViewModel
            {
                HotelId = hotelId,
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(1)
            };
            return View(vm);
        }

        // POST: /Booking/Create
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateViewModel vm)
        {
            var user = userManager.GetUserId(User);
            if (!ModelState.IsValid || userManager.GetUserId(User) == null)
                return View(vm);

            var userId = Guid.Parse(user!);
            var result = await bookingService.CreateBookingAsync(vm, userId);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(vm);
            }

            // Перехід до списку бронювань або підтвердження
            return RedirectToAction("Index", "User"); // або окреслити окремий View
        }
    }
}
