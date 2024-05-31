using AgriEnergyConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    public class MarketplaceViewModel
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public string FilterSeller { get; set; }

        public int FilterCategory { get; set; }

        
        public DateOnly FilterStartDate { get; set; }

        public DateOnly FilterEndDate { get; set; }

        public MarketplaceViewModel()
        {
            
        }

        public MarketplaceViewModel(List<Product> products, List<Category> categories)
        {
            this.Products = products;
            this.Categories = categories;   
        }
    }
}
