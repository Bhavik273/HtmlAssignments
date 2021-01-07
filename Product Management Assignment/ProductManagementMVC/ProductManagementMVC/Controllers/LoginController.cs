using ProductManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProductManagementMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _ApiServer = ConfigurationManager.AppSettings["ApiAddress"].ToString();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login user)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress= new Uri(_ApiServer);
                var model = new ProductManagementModels.Login
                {
                    Password = user.Password,
                    UserName = user.UserName
                };
                var result = client.PostAsync("Login/Authenticate", user, new JsonMediaTypeFormatter()).Result;
                    
                if(result.IsSuccessStatusCode)
                {
                    TempData["MsgType"] = "Success";
                    TempData["Message"] = "Login Sucessful";
                    var userName = result.Content.ReadAsAsync<string>().Result;
                    FormsAuthentication.SetAuthCookie(userName, false);
                    Session["UserName"] = userName;
                    return Redirect("/Product");
                }
                TempData["isSuccess"] = false;
                TempData["Msg"] =  "User Name or Password is Incorrect";
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult AddUser()
        {
            return View("AddUser");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_ApiServer);
                    var entity = new ProductManagementModels.User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        login = new ProductManagementModels.Login
                        {
                            UserName = model.UserName,
                            Password = model.Password
                        }
                    };
                    var result = client.PostAsJsonAsync("User/Add", entity).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["isSuccess"] = true;
                        TempData["Msg"] = "Registration Successful";
                        return RedirectToAction("Index");
                    }
                    else
                        ViewBag.Msg = "Registration Failed";
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}