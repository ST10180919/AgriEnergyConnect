using AgriEnergyConnect.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    //---------------------------------------------------------------------------------
    public class LoginViewModel
    {
        //-----------------------------------------------------------------------------
        /// <summary>
        /// User's email
        /// </summary>
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email Address is in an incorrect format")]
        public string Email { get; set; } = string.Empty;

        //-----------------------------------------------------------------------------
        /// <summary>
        /// User's password
        /// </summary>
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}
//---------------------------------------EOF-------------------------------------------
