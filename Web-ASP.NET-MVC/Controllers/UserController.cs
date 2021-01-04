using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class UserController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(WebUser user)
        {
            if (ModelState.IsValid)
            {
                var checkAccout = db.WebUsers.FirstOrDefault(s => s.Account == user.Account);
                var checkEmail = db.WebUsers.FirstOrDefault(s => s.Email == user.Email);
                if (checkEmail != null)
                {
                    ViewBag.error1 = "Email này đã tồn tại!";
                }
                if (checkAccout == null)
                {
                    user.UserPassword = GetMD5(user.UserPassword);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.WebUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ViewBag.error = "Tên tài khoản này đã tồn tại!";
                    return View();
                }
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Account, string UserPassword)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(UserPassword);
                WebUser user = db.WebUsers.Where(x => x.Account.Equals(Account) && x.UserPassword.Equals(f_password)).FirstOrDefault();
                
                if (String.IsNullOrEmpty(Account))
                {
                    ViewBag.error1 = "Vui lòng nhập tên tài khoản";
                }
                if (String.IsNullOrEmpty(UserPassword))
                {
                    ViewBag.error2 = "Vui lòng nhập mật khẩu";
                }
                else if (user != null)
                {
                    Session["UserId"] = user.UserCode;
                    Session["Account"] = user.Account;
                    Session["FullName"] = user.FullName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    return View("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

    }
}