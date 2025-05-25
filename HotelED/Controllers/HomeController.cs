using HotelED.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers;
[Route("[controller]")]
public class HomeController : Controller
{
    // GET
    [HttpGet("/")]
    [HttpGet("[action]")]
    public IActionResult Index()
    {
        List<Card> hotelTypesCards = [];
        for (var i = 0; i < 10; i++)
        {
            hotelTypesCards.Add(new Card
            {
                Name = "Готель",
                Image = "hotel_exaple.jpeg"
            });
        }

        List<Card> cities = [];
        for (var i = 0; i < 10; i++)
        {
            cities.Add(new Card() { Image = "hotel_exaple.jpeg", Name = "Черкаси" });
        }

        ViewBag.Cities = cities;
        ViewBag.HotelTypesCards = hotelTypesCards;
        return View();
    }
}