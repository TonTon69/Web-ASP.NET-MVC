namespace Web_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductCode { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string ProductDescription { get; set; }

        [Required]
        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Image1 { get; set; }

        [StringLength(250)]
        public string Image2 { get; set; }

        [StringLength(250)]
        public string Image3 { get; set; }

        [StringLength(250)]
        public string Image4 { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [Required]
        public int Quanlity { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(250)]
        public string CreateBy { get; set; }

        public DateTime? TopHot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ProductCetegory ProductCetegory { get; set; }
    }
}
