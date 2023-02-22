using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.WebCore.Models;

namespace PaymentOrder.WebAdminPanel.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel model)
        {
            var user = userManager.FindByNameAsync(model.Email).Result;

            if (user == null)
                return Content("Username or password is incorrect");

            var signInResult = signInManager.PasswordSignInAsync(user, model.Password, true, false).Result;

            if(signInResult.Succeeded)
                return RedirectToAction("Index", "Bank");

            return Content("Username or password is incorrect");
        }

        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return Content("You have no any access for this page");
        }
    }
}
