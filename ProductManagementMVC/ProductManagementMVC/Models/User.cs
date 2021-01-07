using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagementMVC.Models
{
    public class User
    {
        [Required]
        [MinLength(5, ErrorMessage = "Username must of minimum 5 charecter")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be of 8 to 14 character")]
        [MaxLength(14, ErrorMessage = "Password must be of 6 to 14 character")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Password must have uppercase,digit and special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Re-Type Password")]
        [Compare("Password", ErrorMessage = "Password doesnot match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

    }
}