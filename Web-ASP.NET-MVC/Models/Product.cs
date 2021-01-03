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

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm")]
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
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn")]
        public int Quanlity { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int? CategoryCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày khởi tạo")]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập người khởi tạo")]
        [StringLength(250)]
        public string CreateBy { get; set; }

        public DateTime? TopHot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ProductCetegory ProductCetegory { get; set; }
    }
}
