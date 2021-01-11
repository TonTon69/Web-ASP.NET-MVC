namespace Web_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewOrderDetail")]
    public partial class ViewOrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime OrderDay { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool Status { get; set; }

        public bool? Paid { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? Number { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }
    }
}
