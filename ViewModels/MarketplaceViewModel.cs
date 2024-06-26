﻿using AgriEnergyConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// View Model for the marketplace
    /// </summary>
    public class MarketplaceViewModel
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Products being shown on the marketplace
        /// </summary>
        public List<Product> Products { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Categories for the project to filter
        /// </summary>
        public List<Category> Categories { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Field for the user to filter by Seller
        /// </summary>
        public string FilterSeller { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Field for the user to filter by category
        /// </summary>
        public int FilterCategory { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Start date filter
        /// </summary>
        public DateOnly FilterStartDate { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// End date filter
        /// </summary>
        public DateOnly FilterEndDate { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Ctor
        /// </summary>
        public MarketplaceViewModel()
        {
            
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Constructor with products and categories
        /// </summary>
        /// <param name="products"></param>
        /// <param name="categories"></param>
        public MarketplaceViewModel(List<Product> products, List<Category> categories)
        {
            this.Products = products;
            this.Categories = categories;   
        }
    }
}
//---------------------------------------EOF-------------------------------------------