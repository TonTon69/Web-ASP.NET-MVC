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
                obj.Price = pro.Price;
                obj.ProductDescription = pro.ProductDescription;
                obj.Image = pro.Image;
                obj.Quanlity = pro.Quanlity;
                db.Products.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
            //if (fileUpload == null)
            //{
            //    ViewBag.message = "Vui lòng chọn ảnh";
            //    return View();
            //}
            //else
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //Luu ten file
            //        var fileName = Path.GetFileName(fileUpload.FileName);
            //        //Luu duong dan cua file
            //        var path = Path.Combine(Server.MapPath("~/ImageProduct"), fileName);
            //        //Kiem tra image ton tai?
            //        if (System.IO.File.Exists(path))
            //        {
            //            ViewBag.message = "Hình ảnh đã tồn tại";
            //        }
            //        else
            //        {
            //            //Luu anh vao duong dan
            //            fileUpload.SaveAs(path);
            //        }
            //        pro.pro_image = fileName;
            //        //Luu vao CSDL
            //        db.tbl_product.Add(pro);
            //        db.SaveChanges();
            //    }
            //    return RedirectToAction("Index");
            //}
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
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
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
                obj.Price = pro.Price;
                obj.ProductDescription = pro.ProductDescription;
                obj.Image = pro.Image;
                obj.Quanlity = pro.Quanlity;

                UpdateModel(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
            //tbl_product pro = db.tbl_product.Find(id);
            //if (pro == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //if (pro.pro_id != 0)
            //{
            //    db.Entry(pro).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Products");
            //}
            //return View(pro);
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


