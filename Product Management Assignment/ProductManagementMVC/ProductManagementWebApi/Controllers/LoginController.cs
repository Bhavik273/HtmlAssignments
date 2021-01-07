using ProductManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagementWebApi.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        private readonly ProductManagementContext _productManagementContext = new ProductManagementContext();
        // GET api/<controller>
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("Hello");
        }
        [HttpPost]
        [Route("Authenticate")]
        public IHttpActionResult Post(Login login)
        {
            var result = _productManagementContext.LoginEntities
                .FirstOrDefault(entities => login.UserName.Equals(entities.UserName) && login.Password.Equals(entities.Password));

            if (result != null)
                return Ok(login.UserName);
            else
                return NotFound();
        }
    }
}