using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class SlideController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Slide
        public PartialViewResult SlidePartial()
        {
            var slideList = db.Slides.OrderBy(x => x.SlideCode).ToList();
            return PartialView(slideList);
        }
    }
}