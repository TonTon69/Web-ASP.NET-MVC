namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbChiTietDDH")]
    public partial class tbChiTietDDH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MaDDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string MaXe { get; set; }

        public int SoLuong { get; set; }

        public decimal TongTien { get; set; }

        public virtual tbDonDatHang tbDonDatHang { get; set; }

        public virtual tbXe tbXe { get; set; }
    }
}
