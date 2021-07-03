using albelli.datacontract;
using System.Collections.Generic;

namespace albelli.DAL
{
    public interface ICustomerDAL
    {
        /// <summary>
        /// Gets all the customers in the database.
        /// </summary>
        /// <returns></returns>
        List<Customer> GetCustomers();
        /// <summary>
        /// Gets a particular customer.
        /// </summary>
        /// <param name="customerId">
        /// The customer id to get from the database
        /// </param>
        /// <returns>
        /// The particular customer.
        /// </returns>
        Customer GetCustomers(int customerId);
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">
        /// The customer object that needs to be created in the database.
        /// </param>
        /// <returns>
        /// The created customer id.
        /// </returns>
        int CreateCustomer(datacontract.ICustomer customer);
    }
}