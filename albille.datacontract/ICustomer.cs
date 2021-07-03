using System;
using System.Collections.Generic;

namespace albelli.datacontract
{
    public interface ICustomer
    {
        /// <summary>
        /// Gets or sets the customer id
        /// </summary>
        int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        DateTime CreatedDateTime { get; set; }

        ///// <summary>
        ///// Gets or sets the list of customer address.
        ///// </summary>
        //List<CustomerAddress> CustomerAddresses { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        int CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the customers first name
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the customers last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Modified date time.
        /// </summary>
        DateTime ModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        long TelePhone { get; set; }
    }
}