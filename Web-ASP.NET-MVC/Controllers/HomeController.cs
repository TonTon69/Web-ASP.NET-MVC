using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductHighlight()
        {
            var productHighlight = db.Products.OrderByDescending(x => x.ProductCode).Take(5).ToList();
            return PartialView(productHighlight);
        }

        public PartialViewResult ProductNew()
        {
            var productNew = db.Products.OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            return PartialView(productNew);
        }
    }
}