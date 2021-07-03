using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace albelli.datacontract
{
    /// <summary>
    /// Gets or sets the customer address. 
    /// </summary>
    public class CustomerAddress : ICustomerAddress
    {
        /// <summary>
        /// Gets or sets the customer address id.
        /// </summary>
        public int CustomerAddressId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address
        {
            get;
            set;
        }
    }
}
