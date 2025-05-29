using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using HotelED.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers;
[Route("[controller]/[action]")]
public class UserController(IUserService userService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name == null) return RedirectToAction("Login", "Auth");

        var profile = await userService.GetProfileAsync(User.Identity.Name);
        return View(profile);
    }

    public async Task<IActionResult> Bookings()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated || User.Identity.Name == null) return RedirectToAction("Login", "Auth");
        var profile = await userService.GetProfileAsync(User.Identity.Name);
        return View(profile?.Bookings);
    }
}