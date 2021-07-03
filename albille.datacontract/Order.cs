using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace albelli.datacontract
{
    /// <summary>
    /// Gets or sets the Order details for a customer <see cref="Customer"/>.
    /// </summary>
    public class Order : IOrder
    {
        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        public int OrderId
        {
            get;
            set;
        }



        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Order details.
        /// </summary>
        public List<IOrderDetails> OrderDetails
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the minimum width required for the bin
        /// </summary>
        public decimal RequiredBinWidth 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        public int CustomerId
        {
            get;
            set;
        }
    }
}
