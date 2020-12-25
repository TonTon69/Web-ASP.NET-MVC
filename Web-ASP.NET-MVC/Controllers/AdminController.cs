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

namespace Web_ASP.NET_MVC.Controllers
{
    public class AdminController : Controller
    {
        QuanLyShopFashionEntities db = new QuanLyShopFashionEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(int? page)
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_admin avm)
        {
            if (String.IsNullOrEmpty(avm.ad_username) && String.IsNullOrEmpty(avm.ad_password))
            {
                ViewData["error1"] = "Vui lòng nhập tên đăng nhập và mật khẩu";
            }
            else if (String.IsNullOrEmpty(avm.ad_username))
            {
                ViewData["error2"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(avm.ad_password))
            {
                ViewData["error3"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                tbl_admin ad = db.tbl_admin.SingleOrDefault(x => x.ad_username == avm.ad_username && x.ad_password == avm.ad_password);
                if (ad != null)
                {
                    Session["AdminId"] = ad.ad_id.ToString();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
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
        public ActionResult Create(tbl_product pro /*HttpPostedFileBase fileUpload*/)
        {
            if (ModelState.IsValid)
            {
                db.tbl_product.Add(pro);
                db.SaveChanges();
            }
            return RedirectToAction("Products");
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
            //    return RedirectToAction("Products");
            //}
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            //Lay ra doi tuong san pham theo ID
            tbl_product pro = db.tbl_product.SingleOrDefault(x => x.pro_id == id);
            if (pro == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(pro);

        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            //DropDownList 
            var cateList = db.tbl_category.ToList();
            var ctyList = db.tbl_company.ToList();
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
            ViewBag.CtyList = new SelectList(ctyList, "cty_id", "cty_name");

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

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            var pro = db.tbl_product.Find(id);
            if (pro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (pro != null)
            {
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Products");

            }
            return View(pro);
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
            return RedirectToAction("Products");
        }
    }
}


