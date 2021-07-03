using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace albelli.servicelibrary
{
    /// <summary>
    /// Has all the methods thats needed for customer service.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        #region Public Methods
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
        public int Create(datacontract.ICustomer customer)
        {
            int returnValue = Int32.MinValue;

            try
            {
                // Validate he customer object first before updating it.
                if (this.Validate(customer))
                {
                    albelli.DAL.ICustomerDAL customerDAL = core.IoC.Container.Resolve<albelli.DAL.ICustomerDAL>();
                    returnValue = customerDAL.CreateCustomer(customer);
                }

            }
            catch
            {
                //Log the error details here
                throw;
            }

            return returnValue;

        }

        /// <summary>
        /// Gets the customer object from the database based on the id.
        /// </summary>
        /// <param name="id">
        /// Id to find the customer object.
        /// </param>
        /// <returns>
        /// The customer <see cref="datacontract.ICustomer"/> object.
        /// </returns>
        public datacontract.ICustomer GetCustomer(int id)
        {
            try
            {
                datacontract.ICustomer customer = null;
                DAL.ICustomerDAL customerDAL= core.IoC.Container.Resolve<DAL.ICustomerDAL>();
                customer = customerDAL.GetCustomers(id);
                if (customer == null)
                    throw new ArgumentOutOfRangeException("Invalid customer id");
                return customer;
            }
            catch
            {
                //TODO: Should log the error to the file here
                throw;
            }
        }

        /// <summary>
        /// Updates the customer object.
        /// </summary>
        /// <param name="customer">
        /// The customer<see cref="datacontract.ICustomer"/> to update.
        /// </param>
        /// <returns>
        /// Returns true if the Update is sucessfull.
        /// </returns>

        public bool Update(datacontract.ICustomer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the customer (We might need to keep the order history but only delete the customers personal details for GDPR).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Validates the input
        /// </summary>
        /// <param name="customer">
        /// The customer object that needs to be validated.
        /// </param>
        /// <returns>
        /// True if validation is successfull.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If email is empty.
        /// If an email is sent which already exists in the DB.
        /// If first name is empty.
        /// </exception>

        private bool Validate(datacontract.ICustomer customer)
        {
            bool returnValue = true;

            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                throw new ArgumentNullException("First name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentNullException("Email cannot be empty");
            }
            // TODO: Add validation to verify email address already exists.

            return returnValue;

        }

        #endregion

    }
}
