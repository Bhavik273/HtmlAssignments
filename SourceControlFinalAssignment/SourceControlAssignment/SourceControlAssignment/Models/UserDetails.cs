using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SourceControlAssignment.Models
{
    public class UserDetails
    {

        public int ID { get; set; }
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
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set;}

        [Required]
        [RegularExpression(@"^(\+?\d{1,4}[\s-])?(?!0+\s+,?$)\d{10}\s*,?$", ErrorMessage = "Enter Valid Mobile no.")]
        [Display(Name ="Mobile No:")]
        public string Contact { get; set; }
        
        public virtual ICollection<UserImage> ProfilePictures { get; set; }
    }
}