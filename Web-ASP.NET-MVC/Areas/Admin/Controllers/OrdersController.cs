using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //dem so luong don dat hang
            var count = db.FSOrders.Count();
            ViewBag.message = count;

            return View(db.FSOrders.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewOrderDetail orderDetail = db.ViewOrderDetails.SingleOrDefault(x => x.OrderCode == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }
        public PartialViewResult ProductOrderDetail()
        {
            var productOrderDetail = db.ViewOrderDetails.Select(q => q.OrderCode).ToList();
            return PartialView(productOrderDetail);
        }
    }
}