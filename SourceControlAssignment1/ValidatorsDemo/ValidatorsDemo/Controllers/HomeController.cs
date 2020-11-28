using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValidatorsDemo.Models;

namespace ValidatorsDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                TempData["student"] = student;
                return RedirectToAction("Display");
            }

            return View();
        }
        public ActionResult Display()
        {
            Student student = TempData["student"] as Student;
            return View(student);
        }
    }
}