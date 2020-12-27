using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity;
using System.Net;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index(int? page)
        {
            //if (Session["AdminId"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.Products.ToList().OrderBy(n => n.ProductCode).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pro)
        {
            if (ModelState.IsValid)
            {
                Product obj = new Product();
                obj.Name = pro.Name;
                obj.MetaTitle = pro.MetaTitle;
                obj.ProductDescription = pro.ProductDescription;
                obj.Price = pro.Price;
                obj.PromotionPrice = pro.PromotionPrice;
                obj.Image = pro.Image;
                obj.Image1 = pro.Image1;
                obj.Image2 = pro.Image2;
                obj.Image3 = pro.Image3;
                obj.Image4 = pro.Image4;
                obj.Quanlity = pro.Quanlity;
                obj.CreateBy = pro.CreateBy;
                obj.CreatedDate = pro.CreatedDate;
                obj.TopHot = pro.TopHot;
                obj.CategoryID = pro.CategoryID;
                db.Products.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);

        }
        [HttpGet]
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");
            return View(pro);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pro)
        {
            Product obj = new Product();
            if (ModelState.IsValid)
            {
                obj.Name = pro.Name;
                obj.MetaTitle = pro.MetaTitle;
                obj.ProductDescription = pro.ProductDescription;
                obj.Price = pro.Price;
                obj.PromotionPrice = pro.PromotionPrice;
                obj.Image = pro.Image;
                obj.Image1 = pro.Image1;
                obj.Image2 = pro.Image2;
                obj.Image3 = pro.Image3;
                obj.Image4 = pro.Image4;
                obj.Quanlity = pro.Quanlity;
                obj.CreateBy = pro.CreateBy;
                obj.CreatedDate = pro.CreatedDate;
                obj.TopHot = pro.TopHot;
                obj.CategoryID = pro.CategoryID;

                if (pro.CategoryID != 0)
                {
                    db.Entry(obj).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
            }
            return View(pro);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Product pro = db.Products.Find(id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


