using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingWebsite.Models;
using static ShoppingWebsite.Models.Cart;

namespace ShoppingWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private Model2 db = new Model2();


        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Collection);
            Session["product"] = db.Products.ToList();
            return View(products.ToList());
        }

        [Route("product/productdetail/{productID}")]
        public ActionResult ProductDetail(string productID)
        {
            var product = db.Products.Where(p => p.productID == productID).FirstOrDefault();
            return View(product);
        }

        public ActionResult ProductByCollectionID(string collectionID)
        {
            var products = db.Products.Where(p => p.collectionID == collectionID).ToList();
            return View(products);
        }

        [Route("product/addtocart/{productID}")]
        public ActionResult AddToCart(string productID)
        {
            List<Cart> cartList = (List<Cart>)Session["cart"];
            if (cartList == null)
            {
                cartList = new List<Cart>();
                Session["cart"] = cartList;
            }

            var existedProduct = db.Products.FirstOrDefault(p => p.productID == productID);
            if (existedProduct != null)
            {
                // Check if there's already a product with the same properties in the cart
                Cart existingCartItem = cartList.FirstOrDefault(p =>
                    p.ProductID == existedProduct.productID &&
                    p.ProductName == existedProduct.productName &&
                    p.Price == existedProduct.price &&
                    p.Image == existedProduct.image);

                if (existingCartItem != null)
                {
                    // Increase the quantity of the existing item
                    existingCartItem.Quantity++;
                }
                else
                {
                    // Add the new item to the cart
                    cartList.Add(new Cart
                    {
                        ProductID = existedProduct.productID,
                        ProductName = existedProduct.productName,
                        Price = existedProduct.price,
                        Image = existedProduct.image,
                        Quantity = 1
                    });
                }

                // Call the cart updating function
                UpdateCart(cartList);

                // Update Session["cart"] with the modified cartList
                Session["cart"] = cartList;
            }

            return RedirectToAction("Index", "Products");
        }

        // Function to update the cart
        private void UpdateCart(List<Cart> cartList)
        {
            for (int i = 0; i < cartList.Count; i++)
            {
                for (int j = i + 1; j < cartList.Count; j++)
                {
                    // Check if two items have exactly the same properties
                    if (cartList[i].ProductID == cartList[j].ProductID &&
                        cartList[i].ProductName == cartList[j].ProductName &&
                        cartList[i].Price == cartList[j].Price &&
                        cartList[i].Image == cartList[j].Image)
                    {
                        // Increase the quantity of the first item
                        cartList[i].Quantity++;

                        // Remove the later added item
                        cartList.RemoveAt(j);

                        // Update j to account for removed item
                        j--;
                    }
                }
            }
        }

        [Route("product/remove/{productID}")]
        public ActionResult Remove(string productID)
        {
            List<ShoppingWebsite.Models.Cart> cartList = (List<ShoppingWebsite.Models.Cart>)Session["cart"];

            int index = 0;
            while (index < cartList.Count)
            {
                if (cartList[index].ProductID == productID)
                {
                    cartList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Session["cart"] = cartList;

            return RedirectToAction("Cart");
        }


        public ActionResult Cart()
        {
            return View();
        }

        [Route("Products/Details/{id}")]
        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.collectionID = new SelectList(db.Collections, "collectionID", "collectionName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productName,description,price,quantity,image,stockDate,gender,collectionID")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.image = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/images/" + fileName);
                        f.SaveAs(uploadPath);
                        product.image = fileName;
                    }

                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("ProductList", "AdminHome");
            }
            catch (Exception e)
            {
                ViewBag.collectionID = new SelectList(db.Collections, "collectionID", "collectionName", product.collectionID);
                ViewBag.ErrorMessage = e.Message;
                return View(product);
            }
        }

        // GET: Products/Edit/5
        [Route("Products/Edit/{id}")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.collectionID = new SelectList(db.Collections, "collectionID", "collectionName", product.collectionID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Products/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,productName,description,price,quantity,image,stockDate,gender,collectionID")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/images/" + fileName);
                        f.SaveAs(uploadPath);
                        product.image = fileName;
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();       
                }
                return RedirectToAction("ProductList", "AdminHome");
            }
            catch (Exception e)
            {
                ViewBag.collectionID = new SelectList(db.Collections, "collectionID", "collectionName", product.collectionID);
                ViewBag.ErrorMessage = e.Message;
                return View(product);
            }
        }

        // GET: Products/Delete/5
        [Route("Products/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Products/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList", "AdminHome");
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
