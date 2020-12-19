namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbDonDatHang")]
    public partial class tbDonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbDonDatHang()
        {
            tbChiTietDDHs = new HashSet<tbChiTietDDH>();
        }

        [Key]
        [StringLength(50)]
        public string MaDDH { get; set; }

        [Required]
        [StringLength(50)]
        public string DaThanhToan { get; set; }

        [Required]
        [StringLength(100)]
        public string TinhTrangGiaoHang { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime NgayGiao { get; set; }

        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbChiTietDDH> tbChiTietDDHs { get; set; }

        public virtual tbUser tbUser { get; set; }
    }
}
