using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagementMVC.Models
{
    public class CategoryResponseViewModel
    {
        public int id { get; set; }
        public List<string> CategoryList { get; set; }
    }
}