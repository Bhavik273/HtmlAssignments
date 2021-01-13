using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public enum BookingStatus
    {
        Optional,
        Definitive,
        Cancelled,
        Deleted
    }
    public class Booking
    {
        public int Id { get; set; }
        public System.DateTime BookingDate { get; set; }
        public int RoomId { get; set; }
        public BookingStatus Status { get; set; }

        public virtual Room Room { get; set; }
    }
}
