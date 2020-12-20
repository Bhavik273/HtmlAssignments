using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SourceControlAssignment.Models
{
    public class UserImage
    {
        public int ID { get; set; }

        public string ImageName { get; set; }

        public byte[] Image { get; set; }
        public virtual UserDetails User { get; set; }
    }
}