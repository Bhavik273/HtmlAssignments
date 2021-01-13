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
    [MyAuthenticator,RoutePrefix("Hotels")]
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;
        
        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddHotel(Hotel model)
        {
            string msg = _hotelManager.AddHotel(model);
            return Ok(msg);
        }

        [Route("List")]
        public IHttpActionResult GetAllHotels()
        {
            return Ok(_hotelManager.GetHotels());
        }

    }
}
