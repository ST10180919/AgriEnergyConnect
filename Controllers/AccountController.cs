using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModulePlanner.Services;

namespace AgriEnergyConnect.Controllers
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// Controller containing login and register actions
    /// </summary>
    public class AccountController : Controller
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Linking to my custom account service for database and auth functionallity
        /// </summary>
        private readonly IAccountService _accountService;

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">Singleton with app data</param>
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Action returning the initial view of the login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();  
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post action used to submit a user's details from the login form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Action returning the initial view of the register page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post action used to submit a user's details from the register form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            // For some reason an annoying error exists, this fixes it
            ModelState.ClearValidationState("RoleId");
            ModelState.MarkFieldValid("RoleId");

            if (ModelState.IsValid)
            {
                // Hashing password
                // Hashing the password (don't want an unhashed password in memory for long)
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(null, model.Password);
                var user = new Models.User { Email = model.Email, Password = hashedPassword };
                user.Role = this._accountService.GetRoles().FirstOrDefault(r => r.Id == model.RoleId);

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
//---------------------------------------EOF-------------------------------------------