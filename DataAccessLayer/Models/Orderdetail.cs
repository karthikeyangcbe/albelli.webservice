using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    [Keyless]
    [Table("orderdetails")]
    public partial class Orderdetail : IOrderdetail
    {
        [Column("orderdetailsid")]
        public int Orderdetailsid { get; set; }
        [Column("orderid")]
        public int Orderid { get; set; }
        [Column("producttypeid")]
        public int Producttypeid { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
