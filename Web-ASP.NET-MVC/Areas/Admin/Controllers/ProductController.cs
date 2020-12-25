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
        QuanLyShopFashionEntities db = new QuanLyShopFashionEntities();
        public ActionResult Index(int? page)
        {
            //if (Session["AdminId"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.tbl_product.ToList().OrderBy(n => n.pro_id).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //DropDownList 
            var cateList = db.tbl_category.ToList();
            var ctyList = db.tbl_company.ToList();
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
            ViewBag.CtyList = new SelectList(ctyList, "cty_id", "cty_name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_product pro)
        {
            if (ModelState.IsValid)
            {
                tbl_product obj = new tbl_product();
                obj.pro_name = pro.pro_name;
                obj.pro_price = pro.pro_price;
                obj.pro_des = pro.pro_des;
                obj.pro_image = pro.pro_image;
                obj.pro_slton = pro.pro_slton;
                obj.pro_day_update = pro.pro_day_update;
                db.tbl_product.Add(obj);
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
            tbl_product pro = db.tbl_product.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            tbl_product pro = db.tbl_product.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            //DropDownList 
            var cateList = db.tbl_category.ToList();
            var ctyList = db.tbl_company.ToList();
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
            ViewBag.CtyList = new SelectList(ctyList, "cty_id", "cty_name");
            return View(pro);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_product pro)
        {
            //DropDownList 
            var cateList = db.tbl_category.ToList();
            var ctyList = db.tbl_company.ToList();
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
            ViewBag.CtyList = new SelectList(ctyList, "cty_id", "cty_name");

            tbl_product obj = new tbl_product();
            if (ModelState.IsValid)
            {
                obj.pro_name = pro.pro_name;
                obj.pro_price = pro.pro_price;
                obj.pro_des = pro.pro_des;
                obj.pro_image = pro.pro_image;
                obj.pro_slton = pro.pro_slton;
                obj.pro_day_update = pro.pro_day_update;

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
            tbl_product pro = db.tbl_product.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            tbl_product pro = db.tbl_product.Find(id);
            db.tbl_product.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


