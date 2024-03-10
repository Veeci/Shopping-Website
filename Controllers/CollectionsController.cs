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
    public class CollectionsController : Controller
    {
        private Model2 db = new Model2();

        // GET: Collections
        public ActionResult Index()
        {
            return View(db.Collections.ToList());
        }

        // GET: Collections/Details/5
        [Route("Collections/Details/{id}")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "collectionID,collectionName,description")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("CollectionList","AdminHome");
            }

            return View(collection);
        }

        // GET: Collections/Edit/5
        [Route("Collections/Edit/{id}")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Collections/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "collectionID,collectionName,description")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CollectionList", "AdminHome");
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        [Route("Collections/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(collection);
            }
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Collections/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("CollectionList", "AdminHome");
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
