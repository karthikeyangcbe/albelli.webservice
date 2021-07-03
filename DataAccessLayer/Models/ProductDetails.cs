using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    [Table("productdetails")]
    public partial class Productdetails : IProductdetails
    {
        [Key]
        [Column("productid")]
        public short Productid { get; set; }
        [Column("productname")]
        [StringLength(100)]
        public string Productname { get; set; }
        [Column("productdimensioninmm")]
        public decimal Productdimensioninmm { get; set; }
        [Column("stackableupto")]
        public short Stackableupto { get; set; }
    }
}
