using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModels
{
    public class Product
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Required]
        public string SmallImagePath { get; set; }

        public string LargeImagePath { get; set; }
    }
}
