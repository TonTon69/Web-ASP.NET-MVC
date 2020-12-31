namespace Web_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        [StringLength(50)]
        public string UserAdmin { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu ít nhất phải 6 ký tự")]
        [StringLength(50)]
        public string PasswordAdmin { get; set; }

        [StringLength(50)]
        public string FullNameAdmin { get; set; }
    }
}
