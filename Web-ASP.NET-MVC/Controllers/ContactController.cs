using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class ContactController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FeedBack user)
        {
            if (ModelState.IsValid)
            {
                var feeback = new FeedBack();
                feeback.Name = user.Name;
                feeback.Email = user.Email;
                feeback.Address = user.Address;
                feeback.Content = user.Content;
                feeback.Phone = user.Phone;
                feeback.CreateDate = DateTime.Now;
                feeback.FbStatus = false;
                db.FeedBacks.Add(feeback);
                db.SaveChanges();
                ViewBag.Success = "Cảm ơn bạn đã gửi phản hồi cho chúng tôi!";
            }
            ModelState.Clear();
            return View();
        }

    }
}