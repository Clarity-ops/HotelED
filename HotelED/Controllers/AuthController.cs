using HotelED.Core.DTO;
using HotelED.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelED.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
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
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.Values.SelectMany(entry => entry.Errors).Select(error => error.ErrorMessage).ToList();
                return View();
            }

            var user = new ApplicationUser() { Email = registerForm.Email, UserName = registerForm.Email, Name = registerForm.Name };
            var res = await userManager.CreateAsync(user, registerForm.Password);

            if (!res.Succeeded)
            {
                ViewBag.Error = res.Errors.Select(e => e.Description).ToList();
                return View();
            }

            await signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost("")]
        public async Task<IActionResult> Login(LoginForm loginForm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.Values.SelectMany(entry => entry.Errors).Select(error => error.ErrorMessage).ToList();
                return View();
            }

            var res = await signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
            if (!res.Succeeded)
            {
                var errorString = "";
                if (res.IsLockedOut) errorString += "User is locked out\n";
                if (res.IsNotAllowed) errorString += "User is not allowed\n";
                if (res.RequiresTwoFactor) errorString += "User requires Two Factor Authentication";
                if (errorString.Length == 0) errorString = "Invalid Email or Password";
                ViewBag.Error = new List<string> {errorString};
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
