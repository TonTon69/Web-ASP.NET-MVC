using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class CompanyController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Company
        public PartialViewResult CompanyPartial()
        {
            var company = db.Companies.OrderBy(x => x.CompanyCode).ToList();
            return PartialView(company);
        }
    }
}