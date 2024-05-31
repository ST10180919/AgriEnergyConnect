using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    //---------------------------------------------------------------------------------
    public class RegisterViewModel
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// User's email
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        //-----------------------------------------------------------------------------
        /// <summary>
        /// User's password
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; } = 1;

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Confirm password entered by the user
        /// </summary>
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
//---------------------------------------EOF-------------------------------------------
