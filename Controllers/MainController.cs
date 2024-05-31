using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModulePlanner.Services;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace AgriEnergyConnect.Controllers
{
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

        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewData["ActivePage"] = "Dashboard";
            var listedProducts = _accountService.UserLoggedIn.Products.ToList();
            return View(listedProducts);
        }

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

        [HttpGet]
        public IActionResult AddProducts()
        {
            ViewData["ActivePage"] = "Add Products";

            // Initializing model
            var categories = this._accountService.GetCategories();
            var model = new AddProductViewModel(categories);

            return View(model);
        }

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

        [HttpGet]
        public IActionResult Marketplace()
        {
            ViewData["ActivePage"] = "Marketplace";
            var listedProducts = this._accountService.UserLoggedIn.Products.ToList();
            var categories = this._accountService.GetCategories();
            var model = new MarketplaceViewModel(listedProducts, categories);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyFilter(MarketplaceViewModel model)
        {
            var listedProducts = this._accountService.UserLoggedIn.Products.ToList();
            if (model.FilterCategory != null && model.FilterCategory != 0)
            {
                listedProducts = listedProducts.FindAll(p => p.CategoryId == model.FilterCategory);
            }
            if (!model.FilterSeller.IsNullOrEmpty())
            {
                listedProducts = listedProducts.Where(p => p.Seller.Email.Contains(model.FilterSeller)).ToList();
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
    }
}
