using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModulePlanner.Services;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace AgriEnergyConnect.Controllers
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// Controller used to handle all the page actions after login and register
    /// </summary>
    public class MainController : Controller
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Linking to my custom account service for database and auth functionallity
        /// </summary>
        private readonly IAccountService _accountService;

        public MainController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Action returning the inital showing of the dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            var listedProducts = _accountService.UserLoggedIn.Products.ToList();
            var role = _accountService.UserLoggedIn.Role.RoleTitle;
            var model = new DashboardViewModel(listedProducts, role);
            return View(model);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post Action used to remove a listed product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveProduct(int productId)
        {
            // Find Product with productID 
            var product = _accountService.UserLoggedIn.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _accountService.UserLoggedIn.Products.Remove(product);
                this._accountService.RemoveProduct(product);
            }

            return RedirectToAction("Dashboard");
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Action showing the initial state of the AddProducts page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddProducts()
        {
            ViewData["ActivePage"] = "Add Products";

            // Initializing model
            var categories = this._accountService.GetCategories();
            var model = new AddProductViewModel(categories);

            return View(model);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post Action used to add a product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddProducts(AddProductViewModel model)
        {
            // For some reason an annoying error exists, this fixes it
            ModelState.ClearValidationState("Categories");
            ModelState.MarkFieldValid("Categories");

            if (ModelState.IsValid)
            {
                // Converting raw image to byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.RawImage.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    model.Product.Image = imageBytes;
                }
                // Ensuring category is set
                model.Product.Category = _accountService.GetCategories().FirstOrDefault(c => c.Id == model.Product.CategoryId);
                // Saving Changes
                this._accountService.UserLoggedIn.Products.Add(model.Product);
                this._accountService.SaveUserData();
            }

            // Initializing model
            var categories = this._accountService.GetCategories();
            model.Categories = categories;

            return View(model);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Action used to show the inital state of the Marketplace page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Marketplace()
        {
            ViewData["ActivePage"] = "Marketplace";
            var listedProducts = this._accountService.GetAllProducts();
            var categories = this._accountService.GetCategories();
            var model = new MarketplaceViewModel(listedProducts, categories);
            return View(model);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post Action used to apply filters to the Marketplace products
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ApplyFilter(MarketplaceViewModel model)
        {
            var listedProducts = this._accountService.GetAllProducts();
            if (model.FilterCategory != null && model.FilterCategory != 0)
            {
                listedProducts = listedProducts.FindAll(p => p.CategoryId == model.FilterCategory);
            }
            if (!model.FilterSeller.IsNullOrEmpty())
            {
                listedProducts = listedProducts.Where(p => p.Seller.Email.ToLower().Contains(model.FilterSeller.ToLower())).ToList();
            }
            if (model.FilterStartDate != DateOnly.MinValue && model.FilterEndDate != DateOnly.MinValue)
            {
                var start = model.FilterStartDate.ToDateTime(TimeOnly.MinValue);
                var end = model.FilterEndDate.ToDateTime(TimeOnly.MaxValue);
                listedProducts = listedProducts
                    .Where(p => p.ProductionDate.HasValue && p.ProductionDate.Value >= start && p.ProductionDate.Value <= end)
                    .ToList();
            }
            model.Products = listedProducts;
            model.Categories = _accountService.GetCategories();
            return View("Marketplace", model);
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Post Action used to add farmers from the dashboard (For employees)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddFarmer(DashboardViewModel model)
        {
            // For some reason an annoying error exists, this fixes it
            ModelState.ClearValidationState("Products");
            ModelState.MarkFieldValid("Products");
            // For some reason an annoying error exists, this fixes it
            ModelState.ClearValidationState("UserRole");
            ModelState.MarkFieldValid("UserRole");

            if (ModelState.IsValid)
            {
                // Get the farmer details from model
                var email = model.FarmerEmail;
                var hasher = new PasswordHasher<User>();
                var hashedPassword = hasher.HashPassword(null, model.FarmerPassword);

                // Adding user
                var user = new User { Email = email, Password = hashedPassword };
                // Assigning farmer role
                user.Role = _accountService.GetRoles().FirstOrDefault(r => r.Id == 1);
                this._accountService.AddUserWithoutLoggingIn(user);
            }

            var listedProducts = _accountService.UserLoggedIn.Products.ToList();
            var role = _accountService.UserLoggedIn.Role.RoleTitle;
            model = new DashboardViewModel(listedProducts, role);
            return View(nameof(Dashboard), model);
        }
    }
}
//---------------------------------------EOF-------------------------------------------