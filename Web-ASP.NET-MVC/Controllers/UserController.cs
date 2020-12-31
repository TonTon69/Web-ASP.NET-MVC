﻿using System;
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
                var check = db.WebUsers.FirstOrDefault(s => s.Email == user.Email);
                if (check == null)
                {
                    //user.UserPassword = GetMD5(user.UserPassword);
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
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password)
        {
            if (ModelState.IsValid)
            {
                //var f_password = GetMD5(password);
                var data = db.WebUsers.Where(s => s.Account == account && s.UserPassword == password).ToList();
                if (data.Count() > 0)
                {
                    Session["FullName"] = data.FirstOrDefault().FullName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["UserId"] = data.FirstOrDefault().UserCode;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công!";
                    return RedirectToAction("Login", "User");
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
        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");
        //    }
        //    return byte2String;
        //}

    }
}