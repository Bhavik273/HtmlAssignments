using HMS.BAL;
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
    [MyAuthenticator,RoutePrefix("Bookings")]
    public class BookingController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public BookingController(IHotelManager manager)
        {
            _hotelManager = manager;
        }

        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage MakeBooking(Booking booking)
        { 
            BookingStatus status;
            //if (!Enum.TryParse<BookingStatus>(booking.Status, out status))
            //{
            //    var res = Request.CreateResponse(HttpStatusCode.BadRequest);
            //    res.Content = new StringContent("Booking status must be Optional,Definitice,cancelled or deleted");
            //    return res;
            //}
            try
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, _hotelManager.BookRoom(booking.RoomId, booking.BookingDate, booking.Status));
                return response;
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        [Route("UpdateDate")]
        [HttpPut]
        public HttpResponseMessage UpdateBookingDate(Booking booking)
        {
            try
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, _hotelManager.UpdateBookingDate(booking.Id,booking.BookingDate));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("UpdateStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateBookingStatus(Booking booking)
        {
            try
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, _hotelManager.UpdateBookingStatus(booking.Id, booking.Status.ToString()));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("Delete/{id:int}")]
        public HttpResponseMessage DeleteBooking([FromUri]int id)
        {
            try
            {
                var respose = Request.CreateResponse(HttpStatusCode.OK, _hotelManager.DeleteBooking(id));
                return respose;
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
