using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;
using PagedList;
namespace Web_ASP.NET_MVC.Controllers
{
    public class ProductsController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductPartial(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;
            var productList = db.Products.OrderByDescending(x => x.ProductCode).ToPagedList(pageNumber, pageSize);
            return PartialView(productList);
        }
    }
}