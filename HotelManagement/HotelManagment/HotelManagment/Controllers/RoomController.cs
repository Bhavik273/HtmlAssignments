using HMS.BAL.Interface;
using HMS.Models;
using HotelManagment.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagment.Controllers
{
    [MyAuthenticator,RoutePrefix("Rooms")]
    public class RoomController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public RoomController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddRoom(Room model)
        {
            return Ok(_hotelManager.AddRoom(model));
        }

        [Route("List/{listBy=price}")]
        public IHttpActionResult GetAllRoomsBy([FromUri]string listBy)
        {
            return Ok(_hotelManager.GetRooms(listBy));
        }

        [Route("Availability")]
        [HttpGet]
        public HttpResponseMessage CheckAvailability(Booking booking)
        {
            try
            {
                var result = _hotelManager.CheckAvailability(booking.RoomId,booking.BookingDate);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,result);
                return response;
            }
            catch(Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound,ex.Message);
                return response;
            }
        }
    }
}
