﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}

