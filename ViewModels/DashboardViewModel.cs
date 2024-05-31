using AgriEnergyConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    //---------------------------------------------------------------------------------
    /// <summary>
    /// ViewModel for the dashboard view
    /// </summary>
    public class DashboardViewModel
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Products for the user
        /// </summary>
        public List<Product> Products { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// User's role
        /// </summary>
        public string UserRole { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Ctor
        /// </summary>
        public DashboardViewModel()
        {
            
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Farmer email field
        /// </summary>
        [Required]
        public string FarmerEmail { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Farmer password field
        /// </summary>
        [Required]
        public string FarmerPassword { get; set; }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Constructor passing products and user role
        /// </summary>
        /// <param name="products"></param>
        /// <param name="userRole"></param>
        public DashboardViewModel(List<Product> products, string userRole)
        {
            this.Products = products;
            this.UserRole = userRole;
        }
    }
}
//---------------------------------------EOF-------------------------------------------