using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    [Table("order")]
    public partial class Order : IOrder
    {
        public Order()
        {
            this.OrderDetails = new Collection<Orderdetail>();
        }
        [Key]
        [Column("orderid")]
        public int Orderid { get; set; }
        [Column("customerid")]
        public int? Customerid { get; set; }
        [Column("createddatetime", TypeName = "datetime")]
        public DateTime Createddatetime { get; set; }
        public ICollection<Orderdetail> OrderDetails { get; set; }
    }
}
