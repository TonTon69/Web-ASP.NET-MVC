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
            if (ModelState.IsValid)
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


