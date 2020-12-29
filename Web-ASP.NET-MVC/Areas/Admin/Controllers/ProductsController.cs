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
    public class ProductsController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index(int? page, string search)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CurrentFilter = search;
            var products = from s in db.Products select s;
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.Name.Contains(search) || s.ProductCetegory.Name == search);
            }
            products = products.OrderBy(c => c.ProductCode);
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");

            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pro)
        {
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");

            if (ModelState.IsValid)
            {
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);

        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");
            return View(pro);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pro)
        {
            //DropDownList 
            var cateList = db.ProductCetegories.ToList();
            ViewBag.CateList = new SelectList(cateList, "CategoryID", "Name");

            if (ModelState.IsValid)
            {
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Product pro = db.Products.Find(id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ExportContentToExcel()
        {
            var gv = new GridView()
            {
                DataSource = db.Products.OrderBy(x => x.ProductCode).ToList()
            };
            gv.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=ProductsListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var stw = new StringWriter();
            var htmlTw = new HtmlTextWriter(stw);
            gv.RenderControl(htmlTw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
}


