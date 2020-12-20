﻿using SourceControlAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SourceControlAssignment.Controllers
{
    public class UserController : Controller
    {
        UserDB db = new UserDB();

        public object FormAuthentication { get; private set; }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Login login)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(login.UserName) && u.Password.Equals(login.Password));
            if (user == null)
            {
                ViewBag.Message = "User Name or Password is Incorrect";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                UserProfile userProfile = new UserProfile
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    Contact = user.Contact,
                    Image = db.UserImages.FirstOrDefault(u => u.User.ID == user.ID).Image
                };  
                TempData["User"] = userProfile;
                return RedirectToAction("Display");
            }
        }

        public ActionResult Display()
        {
            var user = TempData["User"] as UserProfile;
            if (user == null)
            {
                ViewBag.Message = "User Data not found";
                user = new UserProfile { isEmpty = true };
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDetails user,HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new UserImage
                {
                    ImageName = System.IO.Path.GetFileName(upload.FileName)
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    image.Image = reader.ReadBytes(upload.ContentLength);
                }
                user.ProfilePictures = new List<UserImage> { image };
            }
            db.Users.Add(user);
            db.SaveChanges();
            ViewBag.Success = "Registration Successful";
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}