using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Admin/Company
        public ActionResult Index(string search)
        {
            //dem so luong slide
            var count = db.Slides.Count();
            ViewBag.message = count;

            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CurrentFilter = search;
            var banners = from s in db.Slides select s;
            if (!string.IsNullOrEmpty(search))
            {
                banners = banners.Where(s => s.Discription.Contains(search));
            }
            return View(banners.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slide img)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Slide img = db.Slides.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slide img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Slide");
            }
            return View(img);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide img = db.Slides.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Slide img = db.Slides.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Slide img = db.Slides.Find(id);
            db.Slides.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Index", "Slide");
        }
    }
}