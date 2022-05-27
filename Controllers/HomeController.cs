using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        moviesEntities2 db = new moviesEntities2();
        public ActionResult Index()
        {
            return View(db.movies.ToList());

        }

        //Booking
        [HttpGet]
        public ActionResult Booking()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Booking(booking book)
        {
            var result = db.movies.Where(a => a.m_name == book.movie_name).FirstOrDefault(); 

            // result returns null when the information is not found 
            if (result != null)
            {
                // sessions sends data from a page to another
                Session["z"] = "Booked Successfully!";
                return RedirectToAction("Login_index");
            }
            else
                ModelState.AddModelError("", "invalid movie name");
            return View();
        }
        // Register hyperlink
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user user)
        {
            // lambda operator (=>) used when as a shorthand for declaring objects
            // search the "user" database to make sure that the entered data in the login fields matches the users information in the DB
            // FirstOrDefault() makes sure BOTH fields are correct
            var result = db.users.Where(a => a.username == user.username && a.password == user.password).FirstOrDefault();

            // result returns null when the information is not found 
            if (result != null)
            {
                // sessions sends data from a page to another
                Session["x"] = "Welcome, " + user.username;
                return RedirectToAction("Login_index");
            }
            else
                ModelState.AddModelError("","invalid");
            return View();
        }
        [HttpGet]
        public ActionResult Login_index()
        {
            return View(db.movies.ToList());
        }
        [HttpGet]
        // admin
        public ActionResult Admin_index()
        {
            return View(db.movies.ToList());
        }


        //admin
        [HttpGet]
        public ActionResult admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult admin(admin ad)
        {
            if (ModelState.IsValid)
            {
                var res = db.admins.Where(a => a.username == ad.username && a.admin_pass == ad.admin_pass
                && a.admin_name == ad.admin_name).FirstOrDefault();
                if(res != null)
                {
                    Session["y"] = "Welcome, " + ad.admin_name;
                    return RedirectToAction("admin_index");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(movy m)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(m);
                db.SaveChanges();
                return RedirectToAction("admin_index");
            }
            return View();
        }

        [HttpGet]

        //Editting a movie by admin
        public ActionResult Edit(int id)
        {
            movy m = db.movies.Find(id);
            return View(m);
        }
        [HttpPost]
        public ActionResult Edit(movy m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("admin_index");
            }
            return View();
        }


        //Deleting a movie from the list
        [HttpGet]
        public ActionResult Delete(int id)
        {
            movy m = db.movies.Find(id);
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            movy m = db.movies.Find(id);
            db.movies.Remove(m);
            db.SaveChanges();
            return RedirectToAction("admin_index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}