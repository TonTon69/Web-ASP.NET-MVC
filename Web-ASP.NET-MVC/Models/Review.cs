namespace Web_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [Key]
        public int ReviewCode { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime DatePost { get; set; }

        public int ProductCode { get; set; }

        public virtual Product Product { get; set; }
    }
}
