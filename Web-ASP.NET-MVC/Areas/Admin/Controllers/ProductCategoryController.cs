using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/Category
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index(int? page)
        {
            //if (Session["AdminId"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.ProductCetegories.ToList().OrderBy(n => n.CategoryID).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCetegory cate)
        {
            if (ModelState.IsValid)
            {
                db.ProductCetegories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
           
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCetegory cate)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
           
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            db.ProductCetegories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}