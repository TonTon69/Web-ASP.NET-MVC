using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AccountModel
    {
        private ShopDbContext context = null;
        public AccountModel()
        {
            context = new ShopDbContext();

        }
        //public bool Login(string TenNguoiDung, string MatKhau)
        //{
        //    var res = context.Database.SqlQuery<bool>("")
        //}
    }
}
