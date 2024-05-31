using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModulePlanner.Services;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Linking to my custom account service for database and auth functionallity
        /// </summary>
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();  
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Password = model.Password, Email = model.Email };
                if (this._accountService.LoginCredentialsAreValid(user.Email, user.Password))
                {
                    // Log the user in
                    this._accountService.LoginUser(user);
                    return RedirectToAction("Dashboard", "Main");
                }
                else
                {
                    // Adding invalid credentials error
                    ModelState.AddModelError(nameof(user.Password), "Invalid login credentials");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hashing password
                // Hashing the password (don't want an unhashed password in memory for long)
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(null, model.Password);

                var user = new Models.User { Email = model.Email, Password = hashedPassword };
                if (this._accountService.UserExists(user) == false) // if email not taken
                {
                    this._accountService.RegisterUser(user);
                    return RedirectToAction("Dashboard", "Main");
                }
                else
                {
                    // Adding username taken error
                    ModelState.AddModelError(nameof(user.Email), "This Email is already taken");
                }
            }

            return View(model);
        }
    }
}
