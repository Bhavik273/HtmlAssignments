using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;

namespace HMS.DAL.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Database.HoteManagementEntities _hotelContext;

        public HotelRepository()
        {
            _hotelContext = new Database.HoteManagementEntities();
        }
        public string AddHotel(Hotel model)
        {
            try
            {
                if (model != null)
                {
                    Database.Hotel entity = new Database.Hotel
                    {
                        Name = model.Name,
                        City = model.City,
                        ContactNumber = model.ContactNumber,
                        ContactPerson = model.ContactPerson,
                        Address = model.Address,
                        Pincode = model.Pincode,
                        IsActive = model.IsActive,
                        Facebook = model.Facebook,
                        Twitter = model.Twitter,
                        Website = model.Website,
                        CreateBy = model.CreateBy,
                        CreatedDate = DateTime.Now
                    };

                    _hotelContext.Hotels.Add(entity);
                    _hotelContext.SaveChanges();
                    return "Hotel Added Successfully";
                }
                else
                    return "Model is Null";
            }catch(Exception ex)
            { return ex.ToString(); }
        }

        public string AddRoom(Room model)
        {
            try
            {
                if (model != null)
                {
                    RoomCategory cat;
                    if (!Enum.TryParse<RoomCategory>(model.Category, out cat))
                        return "Category must  be:Category1,Category2 or Category3";

                    Database.Room entity = new Database.Room
                    {
                        Name = model.Name,
                        Category = model.Category.ToString(),
                        IsActive = model.IsActive,
                        Price = model.Price,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = DateTime.Now
                    };
                    var hotelEntity = _hotelContext.Hotels.FirstOrDefault(hotel => hotel.Id == model.HotelId);
                    if (hotelEntity == null)
                        return "Hotel doesnot exist Id=" + model.HotelId;

                    entity.HotelId = model.Id;
                    entity.Hotel = hotelEntity;
                    //Add Room to list of rooms available in hotel
                    hotelEntity.Rooms.Add(entity);
                    _hotelContext.Rooms.Add(entity);
                    _hotelContext.SaveChanges();
                    return "Room Added Successfully";
                }
                else
                    return "Model is Empty";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string BookRoom(int RoomId,DateTime date,BookingStatus bookingStatus = BookingStatus.Optional)
        {
            if(CheckAvailability(RoomId,date))
            {
                var room = _hotelContext.Rooms.FirstOrDefault(r => r.Id == RoomId);
                Database.Booking booking = new Database.Booking
                {
                    BookingDate = date,
                    Room = room,
                    RoomId = room.Id,
                    Status = bookingStatus.ToString()
                };
                _hotelContext.Bookings.Add(booking);
                _hotelContext.SaveChanges();
                return "Booking Done Successfully";
            }
            return "Room with Id:" + RoomId + " is Already booked for date:" + date;
        }

        public bool CheckAvailability(int RoomId, DateTime date)
        {
            //var room = _hotelContext.Rooms.Join(_hotelContext.Bookings, rooms => rooms.Id, bookings => bookings.Id,
            //    (rooms,bookings)=> new
            //    {

            //    });
            var room = _hotelContext.Rooms.FirstOrDefault(r => r.Id == RoomId);
            if(room!=null)
            {
                var booking = _hotelContext.Bookings.FirstOrDefault(b => b.RoomId == RoomId && b.BookingDate.Equals(date));
                return booking == null;
            }
            throw new KeyNotFoundException("No Room with Id:"+RoomId);
        }

        public bool DeleteBooking(int BookingId)
        {
            var booking = _hotelContext.Bookings.FirstOrDefault(b=> b.Id==BookingId);
            if(booking != null)
            {
                booking.Status = BookingStatus.Deleted.ToString();
                _hotelContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Hotel> GetHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            foreach(var model in _hotelContext.Hotels.ToList())
            {
                var hotel = new Hotel
                {
                    Id=model.Id,
                    Name = model.Name,
                    City = model.City,
                    ContactNumber = model.ContactNumber,
                    ContactPerson = model.ContactPerson,
                    Address = model.Address,
                    Pincode = model.Pincode,
                    IsActive = model.IsActive,
                    Facebook = model.Facebook,
                    Twitter = model.Twitter,
                    Website = model.Website,
                    CreateBy = model.CreateBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate =model.UpdatedDate
                };

                hotels.Add(hotel);
            }
            return hotels.OrderBy(h => h.Name).ToList();
        }

        public List<Room> GetRooms(string param = "price")
        {
            List<Room> rooms = new List<Room>();
            foreach (var model in _hotelContext.Rooms.ToList())
            {
                var room = new Room
                {
                    Id = model.Id,
                    Name = model.Name,
                    Category = model.Category,
                    IsActive = model.IsActive,
                    Price = model.Price,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    HotelId = model.HotelId,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    hotel = new Hotel
                    {
                        Id = model.Hotel.Id,
                        Name = model.Hotel.Name,
                        City = model.Hotel.City,
                        ContactNumber = model.Hotel.ContactNumber,
                        ContactPerson = model.Hotel.ContactPerson,
                        Pincode = model.Hotel.Pincode,
                    }
                };
                rooms.Add(room);
            }
            //Sorting
            switch(param.ToLower())
            {
                case "pincode":
                    rooms = rooms.OrderBy(r => r.hotel.Pincode).ToList();
                    break;
                case "city":
                    rooms = rooms.OrderBy(r => r.hotel.City).ToList();
                    break;
                case "price":
                    rooms = rooms.OrderBy(r => r.Price).ToList();
                    break;
                case "category":
                    rooms = rooms.OrderBy(r => r.Category).ToList();
                    break;
                default:
                    rooms = rooms.OrderBy(r => r.Price).ToList();
                    break;
            }
            return rooms;
        }

        public string UpdateBookingDate(int BookingId, DateTime UpdatedDate)
        {
            var booking = _hotelContext.Bookings.FirstOrDefault(b => b.Id == BookingId);
            if (booking != null)
            {
                booking.BookingDate = UpdatedDate;
                _hotelContext.SaveChanges();
                return "Booking Date changed";
            }
            throw new KeyNotFoundException("No Booking with Id:" + BookingId);
        }

        public string UpdateBookingStatus(int BookingId, string updatedBookingStatus)
        {
            if (updatedBookingStatus.ToLower().Equals("definitive") || updatedBookingStatus.ToLower().Equals("cancelled"))
            {
                var booking = _hotelContext.Bookings.FirstOrDefault(b => b.Id == BookingId && !b.Status.ToLower().Equals("deleted"));
                if (booking != null)
                {
                    booking.Status = updatedBookingStatus.ToString();
                    _hotelContext.SaveChanges();
                    return "Booking Date changed";
                }
                throw new KeyNotFoundException("No Booking with Id:" + BookingId);
            }
            throw new InvalidOperationException("Invalid Status value:" + updatedBookingStatus + ":Must be definitive or cancelled");
        }
    }
}
