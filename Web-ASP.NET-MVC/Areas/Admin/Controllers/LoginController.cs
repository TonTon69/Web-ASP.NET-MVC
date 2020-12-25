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
    public class LoginController : Controller
    {
        QuanLyShopFashionEntities db = new QuanLyShopFashionEntities();

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


