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
        [StringLength(50)]
        public string UserAdmin { get; set; }

        [StringLength(50)]
        public string PasswordAdmin { get; set; }

        [StringLength(50)]
        public string FullNameAdmin { get; set; }
    }
}
