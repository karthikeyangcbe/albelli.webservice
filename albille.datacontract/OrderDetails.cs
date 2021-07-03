using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace albelli.datacontract
{
    /// <summary>
    /// Gets or sets the order details for a order
    /// </summary>
    public class OrderDetails : IOrderDetails
    {
        /// <summary>
        /// Gets or sets the order details id.
        /// </summary>
        public int OrderDetailsId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        public int OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        public ProductNames ProductsType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        public short Quantity
        {
            get;
            set;
        }
    }
}
