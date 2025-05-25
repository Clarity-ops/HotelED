using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
