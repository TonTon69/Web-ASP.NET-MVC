using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.ProductCetegories.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCetegory cate)
        {
            if (ModelState.IsValid)
            {
                db.ProductCetegories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
           
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCetegory cate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            ProductCetegory cate = db.ProductCetegories.Find(id);
            db.ProductCetegories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ExportContentToExcel()
        {
            var gv = new GridView()
            {
                DataSource = db.ProductCetegories.OrderBy(x => x.CategoryCode).ToList()
            };
            gv.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=CategoryListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var stw = new StringWriter();
            var htmlTw = new HtmlTextWriter(stw);
            gv.RenderControl(htmlTw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
}