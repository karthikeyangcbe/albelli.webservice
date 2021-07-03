using System;
using System.Collections.Generic;

namespace Albelli.DataAccessLayer.Model
{
    public interface ICustomer
    {
        DateTime Createddate { get; set; }
        int Customerid { get; set; }
        string Email { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        DateTime Modifieddate { get; set; }
        string Password { get; set; }
        long? Telephone { get; set; }
        ICollection<Order> Orders { get; set; }

    }
}