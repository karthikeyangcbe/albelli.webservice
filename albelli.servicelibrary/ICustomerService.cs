using albelli.datacontract;

namespace albelli.servicelibrary
{
    /// <summary>
    /// Has all the methods thats needed for customer service.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">
        /// The new customer to create.
        /// </param>
        /// <returns>
        /// The Id of the newly created user. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If email is empty.
        /// If an email is sent which already exists in the DB.
        /// If first name is empty.
        /// </exception>
        int Create(ICustomer customer);

        /// <summary>
        /// Gets the customer object from the database based on the id.
        /// </summary>
        /// <param name="id">
        /// Id to find the customer object.
        /// </param>
        /// <returns>
        /// The customer <see cref="datacontract.ICustomer"/> object.
        /// </returns>
        datacontract.ICustomer GetCustomer(int id);

        /// <summary>
        /// Updates the customer object.
        /// </summary>
        /// <param name="customer">
        /// The customer<see cref="datacontract.ICustomer"/> to update.
        /// </param>
        /// <returns>
        /// Returns true if the Update is sucessfull.
        /// </returns>
        bool Update(datacontract.ICustomer customer);

        /// <summary>
        /// Deletes the customer (We might need to keep the order history but only delete the customers personal details for GDPR).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}