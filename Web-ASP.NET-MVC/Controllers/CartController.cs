using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ASP.NET_MVC.Models;

namespace Web_ASP.NET_MVC.Controllers
{
    public class CartController : Controller
    {
        ShopFashionContext db = new ShopFashionContext();
        // GET: Cart
        public List<Cart> GetCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }
        public ActionResult AddToCart(int iProductCode, string strURL)
        {
            List<Cart> listCart = GetCart();
            Cart pro = listCart.Find(x => x.iProductCode == iProductCode);
            if (pro == null)
            {
                pro = new Cart(iProductCode);
                listCart.Add(pro);
                return Redirect(strURL);
            }
            else
            {
                pro.iQuantity++;
                return Redirect(strURL);
            }
        }
        private int SumQuantity()
        {
            int iSumQuantity = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                iSumQuantity = listCart.Sum(x => x.iQuantity);
            }
            return iSumQuantity;
        }

        private double SumPrice()
        {
            double iSumPrice = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                iSumPrice = listCart.Sum(x => x.dMoney);
            }
            return iSumPrice;
        }

        public ActionResult Index()
        {
            List<Cart> listCart = GetCart();
            if (listCart.Count == 0)
            {
                return RedirectToAction("CartNull", "Cart");
            }
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();
            return View(listCart);
        }
        public ActionResult CartNull()
        {
            return View();
        }
        public PartialViewResult CartPartial()
        {
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();
            return PartialView();
        }
        public ActionResult Edit(int iProductCode, FormCollection f)
        {
            List<Cart> listCart = GetCart();
            Cart pro = listCart.SingleOrDefault(x => x.iProductCode == iProductCode);
            if (pro != null)
            {
                pro.iQuantity = int.Parse(f["txtQuantity"].ToString());
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Delete(int iProductCode)
        {
            List<Cart> listCart = GetCart();
            Cart pro = listCart.SingleOrDefault(x => x.iProductCode == iProductCode);
            if (pro != null)
            {
                listCart.RemoveAll(x => x.iProductCode == iProductCode);
                return RedirectToAction("Index", "Cart");
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Products");
            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Products");
            }

            List<Cart> listCart = GetCart();
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();

            return View(listCart);
        }
        [HttpPost]
        public ActionResult Checkout(FormCollection collection)
        {
            FSOrder ddh = new FSOrder();
            WebUser user = (WebUser)Session["Account"];
            List<Cart> listCart = GetCart();
            ddh.UserCode = user.UserCode;
            ddh.OrderDay = DateTime.Now;

            var deliveryDay = String.Format("{0:MM/dd/yyyy}", collection["DeliveryDay"]);
            ddh.DeliveryDay = DateTime.Parse(deliveryDay);
            //tinh trang giao hang
            ddh.Status = false;
            //da thanh toan
            ddh.Paid = false;
            db.FSOrders.Add(ddh);
            db.SaveChanges();

            //them CTDH
            foreach (var item in listCart)
            {
                OrderDetail ctdh = new OrderDetail();
                ctdh.OrderCode = ctdh.OrderCode;
                ctdh.ProductCode = item.iProductCode;
                ctdh.Number = item.iQuantity;
                ctdh.TotalPrice = (decimal)item.dPrice;
                db.OrderDetails.Add(ctdh);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("ConfirmOrder", "Cart");
        }
        public ActionResult ConfirmOrder(FormCollection collection)
        {
            return View();
        }
    }
}