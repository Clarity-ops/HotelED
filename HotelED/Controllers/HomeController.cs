using HotelED.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers;
[Route("[controller]")]
[AllowAnonymous]
public class HomeController(IHotelService hotelService) : Controller
{
    // GET
    [HttpGet("/")]
    [HttpGet("[action]/{location?}")]
    public async Task<IActionResult> Index(string? location)
    {
        var hotels = (await hotelService.GetAllHotelsAsync()).ToList();
        if (location is null) return View(hotels);
        var filtered = hotels.Where(hotel => hotel.Location.Contains(location));
        return View(filtered);

    }
}