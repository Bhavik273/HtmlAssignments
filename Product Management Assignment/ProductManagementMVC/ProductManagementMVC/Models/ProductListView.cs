using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagementMVC.Models
{
    public class ProductListView
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string SmallImagePath { get; set; }
    }
}