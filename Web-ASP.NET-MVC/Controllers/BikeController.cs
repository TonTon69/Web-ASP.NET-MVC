using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ASP.NET_MVC.Controllers
{
    public class BikeController : Controller
    {
        // GET: Bike
        public ActionResult Index()
        {
            return View();
        }
    }
}