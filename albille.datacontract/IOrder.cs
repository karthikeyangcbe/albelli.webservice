using System;
using System.Collections.Generic;

namespace albelli.datacontract
{
    public interface IOrder
    {
        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Order details.
        /// </summary>
        List<IOrderDetails> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        int OrderId { get; set; }

        /// <summary>
        /// Gets the minimum required width for bin.
        /// </summary>
        decimal RequiredBinWidth { get; set; }

        /// <summary>
        /// Gets or sets the Customer ID.
        /// </summary>
        int CustomerId { get; set; }
    }
}