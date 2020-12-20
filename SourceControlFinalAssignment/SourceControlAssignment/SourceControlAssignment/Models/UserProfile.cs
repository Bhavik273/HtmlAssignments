using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAssignment.Models
{
    public class UserProfile
    {
        [Display(Name="User Name")]
        public string UserName{get; set; }
        
        public string Name { get; set; }

        [Display(Name ="Mobile No:")]
        public string Contact { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Image { get; set; }

        [ScaffoldColumn(false)]
        public bool isEmpty { get; set; }
    }
}