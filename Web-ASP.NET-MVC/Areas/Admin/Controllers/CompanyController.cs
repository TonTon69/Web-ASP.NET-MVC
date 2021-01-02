using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Admin/Company
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Company cty = db.Companies.Find(id);
            if (cty == null)
            {
                return HttpNotFound();
            }
            return View(cty);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company cty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Company");
            }
            return View(cty);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Company cty = db.Companies.Find(id);
            if (cty == null)
            {
                return HttpNotFound();
            }
            return View(cty);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Company cty = db.Companies.Find(id);
            db.Companies.Remove(cty);
            db.SaveChanges();
            return RedirectToAction("Index", "Company");
        }
    }
}