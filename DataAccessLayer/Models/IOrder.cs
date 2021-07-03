using System;
using System.Collections.Generic;

namespace Albelli.DataAccessLayer.Model
{
    public interface IOrder
    {
        DateTime Createddatetime { get; set; }
        int? Customerid { get; set; }
        int Orderid { get; set; }
        ICollection<Orderdetail> OrderDetails { get; set; }

    }
}