using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShoppingWebsite.Models;

namespace ShoppingWebsite.Controllers
{
    public class AdminLoginController : Controller
    {
        private Model1 db = new Model1();

        // GET: AdminLogin
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                return View(db.admins.ToList());
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(db.admins.ToList());
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
                var admin = db.admins.Where(a => a.username == username && a.password == password).FirstOrDefault();
                if (admin != null)
                {
                    Session["admin"] = admin.full_name;
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password!";
                    return View("Login", admin);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("admin");
            return RedirectToAction("Login", "AdminLogin");
        }

        // GET: AdminLogin/Details/5
        [Route("Admins/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: AdminLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(admin admin)
        {
            bool result;
            try
            {
                db.admins.Add(admin);
                db.SaveChanges();
                result = true;
                return View();
            }
            catch (Exception ex)
            {
                result = false;
                var error = ex.Message;
                return View(admin);
            }
        }

        // GET: AdminLogin/Edit/5
        [Route("Admins/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: AdminLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Admins/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminid,username,password,full_name")] admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: AdminLogin/Delete/5
        [Route("Admins/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: AdminLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Admins/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin admin = db.admins.Find(id);
            db.admins.Remove(admin);
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
