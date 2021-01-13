using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IHotelManager
    {
        string AddHotel(Hotel model);
        string AddRoom(Room model);
        List<Hotel> GetHotels();

        List<Room> GetRooms(string param = null);

        bool CheckAvailability(int RoomId, DateTime date);

        string BookRoom(int RoomId,DateTime date, BookingStatus bookingStatus = BookingStatus.Optional);

        string UpdateBookingDate(int BookingId, DateTime UpdatedDate);

        string UpdateBookingStatus(int BookingId, string updatedBookingStatus);

        bool DeleteBooking(int BookingId);
    }
}
