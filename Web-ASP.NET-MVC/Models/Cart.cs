using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_ASP.NET_MVC.Models
{
    public class Cart
    {
        ShopFashionContext db = new ShopFashionContext();
        public int iProductCode { get; set; }
        public string sProductName { get; set; }
        public string sImage { get; set; }
        public Double dPrice { get; set; }
        public int iQuantity { get; set; }
        public Double dMoney
        {
            get { return iQuantity * dPrice; }
        }

        public Cart(int ProductCode)
        {
            iProductCode = ProductCode;
            Product pro = db.Products.Single(x => x.ProductCode == iProductCode);
            sProductName = pro.Name;
            sImage = pro.Image;
            dPrice = Double.Parse(pro.Price.ToString());
            iQuantity = 1;
        }
    }
}