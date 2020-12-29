using System;
using System.Linq;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;


namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Administrator avm)
        {
            if (String.IsNullOrEmpty(avm.UserAdmin) && String.IsNullOrEmpty(avm.PasswordAdmin))
            {
                ViewData["error1"] = "Vui lòng nhập tên đăng nhập và mật khẩu";
            }
            else if (String.IsNullOrEmpty(avm.UserAdmin))
            {
                ViewData["error2"] = "Vui lòng nhập tên tài khoản";
            }
            else if (String.IsNullOrEmpty(avm.PasswordAdmin))
            {
                ViewData["error3"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                Administrator ad = db.Administrators.Where(x => x.UserAdmin == avm.UserAdmin && x.PasswordAdmin == avm.PasswordAdmin).FirstOrDefault();
                if (ad != null)
                {
                    Session["AdminId"] = ad.UserAdmin;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["AdminId"] = "";
            return RedirectToAction("Index", "Login");
        }


    }
}


