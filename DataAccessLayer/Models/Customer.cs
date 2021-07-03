using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    [Table("customer")]
    public partial class Customer : ICustomer
    {
        [Key]
        [Column("customerid")]
        public int Customerid { get; set; }
        [Required]
        [Column("firstname")]
        [StringLength(100)]
        public string Firstname { get; set; }
        [Column("lastname")]
        [StringLength(100)]
        public string Lastname { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("password")]
        [MaxLength(100)]
        public string Password { get; set; }
        [Column("telephone")]
        public long? Telephone { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("modifieddate", TypeName = "datetime")]
        public DateTime Modifieddate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
