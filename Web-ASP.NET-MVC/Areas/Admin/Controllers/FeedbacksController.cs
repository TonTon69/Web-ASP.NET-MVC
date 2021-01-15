using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class FeedbacksController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Admin/Feedbacks
        public ActionResult Index()
        {
            //dem so luong 
            var count = db.FeedBacks.Count();
            ViewBag.message = count;

            return View(db.FeedBacks.ToList());
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            FeedBack fb = db.FeedBacks.Find(id);
            if (fb == null)
            {
                return HttpNotFound();
            }
            return View(fb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            FeedBack fb = db.FeedBacks.Find(id);
            db.FeedBacks.Remove(fb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}