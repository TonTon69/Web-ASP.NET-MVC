using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var count = db.OrderDetails.Count();
            ViewBag.message = count;
            return View(db.OrderDetails.ToList());
        }
    }
}