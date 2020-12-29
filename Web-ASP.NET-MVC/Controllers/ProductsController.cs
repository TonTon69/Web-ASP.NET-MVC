using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;
using PagedList;
using System.Net;

namespace Web_ASP.NET_MVC.Controllers
{
    public class ProductsController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductPartial(int? page, string search)
        {
            var products = from s in db.Products select s;
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.Name.Contains(search) || s.ProductCetegory.Name == search);
            }
            products = products.OrderByDescending(x => x.ProductCode);
            int pageNumber = page ?? 1;
            int pageSize = 6;
            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

    }
}