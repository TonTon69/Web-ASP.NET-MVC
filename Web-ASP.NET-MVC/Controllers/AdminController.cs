using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

using PagedList;
using PagedList.Mvc;
using System.IO;

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
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.tbl_product.ToList().OrderBy(n => n.pro_id).ToPagedList(pageNumber, pageSize));
        }
       
        [HttpPost]
        public ActionResult Create(tbl_product pro, HttpPostedFileBase fileUpload)
        {
            //Muốn thêm mới trước hết phải login
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login");
            }

            //Luu ten file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Luu duong dan cua file
            var path = Path.Combine(Server.MapPath("~/ImageProduct"), fileName);
            //Kiem tra image ton tai?
            if (System.IO.File.Exists(path))
            {
                ViewBag.message = "Hình ảnh đã tồn tại";
            }
            else
            {
                //Luu anh vao duong dan
                fileUpload.SaveAs(path);
            }

            //DropDownList 
            var cateList = db.tbl_category.ToList();
            var adList = db.tbl_admin.ToList();
            var ctyList = db.tbl_company.ToList();
            ViewBag.CateList = new SelectList(cateList, "cat_id", "cat_name");
            ViewBag.AdList = new SelectList(adList, "ad_id", "id_username");
            ViewBag.CtyList = new SelectList(ctyList, "cty_id", "cty_name");
            return View();
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

    }
}


