namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbXe")]
    public partial class tbXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbXe()
        {
            tbChiTietDDHs = new HashSet<tbChiTietDDH>();
        }

        [Key]
        [StringLength(50)]
        public string MaXe { get; set; }

        [Required]
        [StringLength(50)]
        public string TenXe { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiXe { get; set; }

        public decimal GiaBan { get; set; }

        [Required]
        [StringLength(2000)]
        public string MoTa { get; set; }

        public int SoLuongTon { get; set; }

        [StringLength(50)]
        public string MaNCC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbChiTietDDH> tbChiTietDDHs { get; set; }

        public virtual tbNhaCungCap tbNhaCungCap { get; set; }
    }
}
