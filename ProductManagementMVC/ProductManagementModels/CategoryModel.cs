using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModels
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Index("InxProductCategory", IsUnique = true)]
        [MaxLength(200)]
        public string ProductCategory { get; set; }
    }
}
