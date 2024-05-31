using AgriEnergyConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// ViewModel for the AddProduct Page
    /// </summary>
    public class AddProductViewModel
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Product being added
        /// </summary>
        public Product Product { get; set; } = new Product();

        //-----------------------------------------------------------------------------
        /// <summary>
        /// All Catgories for user to choose from
        /// </summary>
        public List<Category> Categories { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// RawImage added by user
        /// </summary>
        [Required(ErrorMessage = "Please enter an image for your Product")]
        public IFormFile RawImage { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Constructor passing categories
        /// </summary>
        /// <param name="categories"></param>
        public AddProductViewModel(List<Category> categories)
        {
            this.Categories = categories;
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Ctor
        /// </summary>
        public AddProductViewModel()
        {
            
        }
    }
}
//---------------------------------------EOF-------------------------------------------