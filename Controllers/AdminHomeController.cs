using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWebsite.Models;
using System.Data.Entity;

namespace ShoppingWebsite.Controllers
{
    public class AdminHomeController : Controller
    {
        Model2 db = new Model2();

        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products
        public ActionResult ProductList()
        {
            if (Session["admin"] != null)
            {
                return View(db.Products.ToList());
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        // GET: Collections
        public ActionResult CollectionList()
        {
            if (Session["admin"] != null)
            {
                return View(db.Collections.ToList());
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }
    }
}