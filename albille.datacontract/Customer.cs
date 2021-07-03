using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace albelli.datacontract
{
    /// <summary>
    /// Contains all the attributes of the customer.
    /// </summary>
    public class Customer : ICustomer
    {
        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        public int CustomerId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public int CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customers first name
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customers last name.
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the encrypted password
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public long TelePhone
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
        /// Gets or sets the Modified date time.
        /// </summary>
        public DateTime ModifiedDateTime
        {
            get;
            set;
        }

        /// <summary>
        ///// Gets or sets the list of customer address.
        ///// </summary>
        //public List<CustomerAddress> CustomerAddresses
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Gets all the orders<see cref="Order"/> for this customer instance. 
        ///// We dont want to reutrn this in WebAPI response since if a customer has more orders we dont want to bloat the JSON.
        ///// If Order details are needed for a customer a seperate request should be made.
        ///// </summary>
        //[System.Text.Json.Serialization.JsonIgnore]
        //public List<Order> Orders
        //{
        //    get;
        //}
    }
}
