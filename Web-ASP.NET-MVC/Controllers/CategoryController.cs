using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class CategoryController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: CategoryPartial
        public PartialViewResult CategoryPartial()
        {
            var categoryList = db.ProductCetegories.OrderBy(x => x.CategoryCode).ToList();
            return PartialView(categoryList);
        }
    }
}