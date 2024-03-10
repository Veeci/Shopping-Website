using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite.Models
{
    public class Cart
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}