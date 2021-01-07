using ProductManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagementWebApi.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly ProductManagementContext _productManagementContext = new ProductManagementContext();

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddUser(User model)
        {
            _productManagementContext.UserEntities.Add(model);
            _productManagementContext.SaveChanges();
            return Ok();
        }
    }
}
