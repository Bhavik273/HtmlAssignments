using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public enum RoomCategory{
        Category1,
        Category2,
        Category3
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel hotel { get; set; }
    }
}
