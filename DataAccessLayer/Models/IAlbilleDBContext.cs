using Microsoft.EntityFrameworkCore;

namespace Albelli.DataAccessLayer.Model
{
    public interface IAlbilleDBContext
    {
        DbSet<Customeraddress> Customeraddresses { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Orderdetail> Orderdetails { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Productdetails> Productnames { get; set; }
    }
}