using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class FeedbacksController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Admin/Feedbacks
        public ActionResult Index(int? page)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //dem so luong 
            var count = db.FeedBacks.Count();
            ViewBag.message = count;

            var feedbacks = from s in db.FeedBacks select s;

            feedbacks = feedbacks.OrderBy(c => c.FeedBackCode);
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(feedbacks.ToPagedList(pageNumber, pageSize));
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            FeedBack fb = db.FeedBacks.Find(id);
            if (fb == null)
            {
                return HttpNotFound();
            }
            return View(fb);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                FeedBack fb = db.FeedBacks.SingleOrDefault(x => x.FeedBackCode == id);
                if (fb != null)
                {
                    fb.FbStatus = true;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public void ExportContentToExcel()
        {
            var gv = new GridView()
            {
                DataSource = db.FeedBacks.OrderBy(x => x.FeedBackCode).ToList()
            };
            gv.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=FeedbackListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var stw = new StringWriter();
            var htmlTw = new HtmlTextWriter(stw);
            gv.RenderControl(htmlTw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
}