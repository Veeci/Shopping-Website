using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingWebsite.Models;

namespace ShoppingWebsite.Controllers
{
    public class UserLoginController : Controller
    {
        private Model1 db = new Model1();

        // GET: UserLogin
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }                      

        [HttpGet]
        public ActionResult Login()
        {
            return View(db.users.ToList());
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username.Equals("") || password.Equals(""))
            {
                ViewBag.Error = "Username or Password is empty!";
                return View("Login");
            }
            else
            {
                var user = db.users.Where(u => u.username == username && u.password == password).FirstOrDefault();
                if (user != null)
                {
                    Session["user"] = user.full_name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password!";
                    return View("Login", user);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }

        // GET: UserLogin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: UserLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(user user)
        {
            bool result;
            try
            {
                db.users.Add(user);
                db.SaveChanges();
                result = true;
                ViewBag.SuccessMessage = "New record added successfully!";
                return View();
            }
            catch (Exception ex)
            {
                result = false;
                var error = ex.Message;
                ModelState.AddModelError("", "An error occurred while adding the user.");
                return View(user);
            }
        }


        // GET: UserLogin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UserLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,username,password,email,phone_number,full_name")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: UserLogin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UserLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
