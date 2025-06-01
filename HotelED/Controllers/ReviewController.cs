using HotelED.Core.DTO;
using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers
{
    [Route("[controller]/[action]")]
    public class ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager) : Controller
    {
        // POST: /Review/Create
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Якщо форма некоректна, повертаємося назад на сторінку готелю з повідомленням
                TempData["ReviewError"] = "Будь ласка, заповніть усі поля коректно.";
                return RedirectToAction("Details", "Hotel", new { id = vm.HotelId });
            }

            var userId = Guid.Parse(userManager.GetUserId(User) ?? string.Empty);
            var result = await reviewService.CreateReviewAsync(vm, userId);
            if (!result.Success)
            {
                TempData["ReviewError"] = result.Message;
            }
            else
            {
                TempData["ReviewSuccess"] = "Дякуємо за ваш відгук!";
            }

            return RedirectToAction("Details", "Hotel", new { id = vm.HotelId });
        }
    }
}
