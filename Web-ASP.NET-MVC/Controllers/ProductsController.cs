using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;
using PagedList;
using System.Net;

namespace Web_ASP.NET_MVC.Controllers
{
    public class ProductsController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductPartial(int? page, string search, string currentFilter, int? category)
        {
            ViewBag.CurrentFilter = search;

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            var products = from s in db.Products select s;

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.Name.Contains(search) || s.ProductCetegory.Name.Contains(search));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            if (category != null)
            {
                ViewBag.category = category;
                ViewBag.page = page;
                products = products.OrderByDescending(x => x.ProductCode).Where(x => x.CategoryCode == category);
                return PartialView(products.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                products = products.OrderByDescending(x => x.ProductCode);
                return PartialView(products.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Details(string name, int? id)
        {
            var sumComment = db.Reviews.Count(t => t.ProductCode == id).ToString();
            if (sumComment != null)
            {
                ViewBag.message = sumComment;
            }
            else
            {
                ViewBag.message = 0;
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        public PartialViewResult ProductRelated(int id)
        {
            var productRelated = db.Products.Where(x => x.ProductCode != id && x.CategoryCode == id).ToList();
            return PartialView(productRelated);
        }
        //Comment
        public PartialViewResult ShowComment(int id)
        {
            var commentShow = db.Reviews.Where(t => t.ProductCode == id).ToList();

            ViewBag.productCode = id;
            return PartialView(commentShow);
        }
        public ActionResult LeaveComment(int productCode, int rating, string txtComment)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var objReview = new Review();
            objReview.Content = txtComment;
            objReview.Rating = rating;
            objReview.DatePost = DateTime.Now;
            objReview.ProductCode = productCode;
            db.Reviews.Add(objReview);
            db.SaveChanges();
            return RedirectToAction("Details", "Products", new { id = productCode });
        }

    }
}