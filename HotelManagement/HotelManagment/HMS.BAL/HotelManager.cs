using HMS.BAL.Interface;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelManager(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public string AddHotel(Hotel model)
        {
            return _hotelRepository.AddHotel(model);
        }

        public string AddRoom(Room model)
        {
            return _hotelRepository.AddRoom(model);
        }

        public string BookRoom(int RoomId,DateTime date, BookingStatus bookingStatus = BookingStatus.Optional)
        {
            return _hotelRepository.BookRoom(RoomId,date, bookingStatus);
        }

        public bool CheckAvailability(int RoomId, DateTime date)
        {
            return _hotelRepository.CheckAvailability(RoomId, date);
        }

        public bool DeleteBooking(int BookingId)
        {
            return _hotelRepository.DeleteBooking(BookingId);
        }

        public List<Hotel> GetHotels()
        {
            return _hotelRepository.GetHotels();
        }

        public List<Room> GetRooms(string param = null)
        {
            return _hotelRepository.GetRooms(param);
        }

        public string UpdateBookingDate(int BookingId, DateTime UpdatedDate)
        {
            return _hotelRepository.UpdateBookingDate(BookingId, UpdatedDate);
        }

        public string UpdateBookingStatus(int BookingId, string updatedBookingStatus)
        {
            return _hotelRepository.UpdateBookingStatus(BookingId, updatedBookingStatus);
        }
    }
}
