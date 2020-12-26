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
    public class CategoryController : Controller
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
                ProductCetegory obj = new ProductCetegory();
                obj.Name = pro.Name;
                db.ProductCetegories.Add(obj);
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
            ProductCetegory pro = db.ProductCetegories.Find(id);
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
            ProductCetegory obj = new ProductCetegory();
            if (ModelState.IsValid)
            {
                obj.Name = pro.Name;

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
            ProductCetegory pro = db.ProductCetegories.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            ProductCetegory pro = db.ProductCetegories.Find(id);
            db.ProductCetegories.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}