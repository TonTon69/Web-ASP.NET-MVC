﻿namespace Web_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WebUser")]
    public partial class WebUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WebUser()
        {
            FSOrders = new HashSet<FSOrder>();
        }

        [Key]
        public int UserCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        [StringLength(50)]
        public string Account { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Xác nhận mật khẩu")]
        [Compare("UserPassword")]
        [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [StringLength(100)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Vui lòng nhập email hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(250)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(11)]
        public string Phone { get; set; }
        public DateTime? BirthDay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FSOrder> FSOrders { get; set; }
    }
}
