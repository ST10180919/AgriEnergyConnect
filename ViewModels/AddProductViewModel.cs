using AgriEnergyConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    public class AddProductViewModel
    {
        public Product Product { get; set; } = new Product();

        public List<Category> Categories { get; set; }

        [Required(ErrorMessage = "Please enter an image for your Product")]
        public IFormFile RawImage { get; set; }

        public AddProductViewModel(List<Category> categories)
        {
            this.Categories = categories;
        }

        public AddProductViewModel()
        {
            
        }
    }
}
