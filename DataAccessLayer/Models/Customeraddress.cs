using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    [Table("customeraddresses")]
    public partial class Customeraddress : ICustomeraddress
    {
        [Key]
        [Column("customeraddressid")]
        public int Customeraddressid { get; set; }
        [Column("customerid")]
        public int Customerid { get; set; }
        [Column("address")]
        [StringLength(250)]
        public string Address { get; set; }
    }
}
