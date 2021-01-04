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
                return RedirectToAction("Index", "Products");
            }
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();
            return View(listCart);
        }

        public PartialViewResult CartPartial()
        {
            ViewBag.SumQuantity = SumQuantity();
            ViewBag.SumPrice = SumPrice();
            return PartialView();
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
    }
}