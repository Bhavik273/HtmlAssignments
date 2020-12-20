using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAssignment.Models
{
    public class Login
    {
        [Required
        [MinLength(5,ErrorMessage ="Username must of minimum 5 charecter")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Password must be of 8 to 14 character")]
        [MaxLength(14,ErrorMessage = "Password must be of 6 to 14 character")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage ="Password must have uppercase,digit and special character")]
        public string Password { get; set; }

        [Required]
        [Display(Name="Re-Type Password")]
        [Compare(nameof(Password),ErrorMessage ="Password doesnot match")]
        public string ConfirmPassword { get; set; }
    }
}