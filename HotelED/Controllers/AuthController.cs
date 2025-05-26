using HotelED.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HotelED.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("")]
        public IActionResult Register(RegisterForm registerForm)
        {
            var modle = ModelState;
            return Empty;
        }
        [HttpPost("")]
        public IActionResult Login(LoginForm loginForm)
        {
            return Empty;
        }
    }
}
