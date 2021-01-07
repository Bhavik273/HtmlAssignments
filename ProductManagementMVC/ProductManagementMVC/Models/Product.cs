using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagementMVC.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price must be >0" )]
        public int Price { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be >0")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name="Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name="Long Description")]
        public string LongDescription { get; set; }

        [ScaffoldColumn(false)]
        public string SmallImagePath { get; set; }


        [ScaffoldColumn(false)]
        public string LargeImagePath { get; set; }

    }
}