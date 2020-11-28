using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ValidatorsDemo.Custom_Validators;

namespace ValidatorsDemo.Models
{
    public class Student
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter position")]
        [DepartmentValidator]
        public string Department { get; set; }


        [Required(ErrorMessage = "Please enter Birth date")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [BirthDate(ErrorMessage = "Birthdate Date must be less than or equal to Today's Date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Enter Semester")]
        [Range(1,8,ErrorMessage ="Value must be between 1 and 8")]
        public int Semester { get; set; }

        [Required(ErrorMessage ="Enter Gender")]
        [RegularExpression("^Male|Female$",ErrorMessage ="Must be Male or Female")]
        public string Gender { get; set; }
    }

    public class DepartmentValidator : ValidationAttribute
    {
        private string[] Departments = { "CE", "EC", "IT" };
        public override bool IsValid(object value)
        {
            return Departments.Contains(value.ToString());
        }
    }
}