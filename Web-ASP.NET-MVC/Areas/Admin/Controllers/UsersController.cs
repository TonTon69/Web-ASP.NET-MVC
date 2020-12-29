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
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index(int? page, string search)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CurrentFilter = search;
            var users = from s in db.WebUsers select s;
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(s => s.FullName.Contains(search) || s.Email == search);
            }
            users = users.OrderBy(c => c.UserCode);
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebUser user)
        {
            if (ModelState.IsValid)
            {
                db.WebUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);

        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebUser user = db.WebUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            WebUser user = db.WebUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WebUser user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            WebUser user = db.WebUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            WebUser user = db.WebUsers.Find(id);
            db.WebUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ExportContentToExcel()
        {
            var gv = new GridView()
            {
                DataSource = db.WebUsers.OrderBy(x => x.UserCode).ToList()
            };
            gv.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=UsersListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var stw = new StringWriter();
            var htmlTw = new HtmlTextWriter(stw);
            gv.RenderControl(htmlTw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
}


