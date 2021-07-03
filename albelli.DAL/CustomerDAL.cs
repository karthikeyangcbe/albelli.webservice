using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace albelli.DAL
{
    /// <summary>
    /// Has the data access methods related to customers.
    /// </summary>
    public class CustomerDAL : ICustomerDAL
    {
        #region Public Methods
        /// <summary>
        /// Reterives the all the customers
        /// </summary>
        /// <returns></returns>
        public List<albelli.datacontract.Customer> GetCustomers()
        {
            return this.GetCustomerDetails(null);
        }
        /// <summary>
        /// Reterives a specific customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public albelli.datacontract.Customer GetCustomers(int customerId)
        {
            return this.GetCustomerDetails(customerId).FirstOrDefault();
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer">
        /// The customer object that needs to be created in database
        /// </param>
        /// <returns>
        /// The customer id.
        /// </returns>
        public int CreateCustomer(datacontract.ICustomer customer)
        {
            string sqlQuery = $"INSERT INTO [dbo].[customer] ([firstname],[lastname],[email],[password],[telephone],[createddate],[modifieddate]) output INSERTED.customerid VALUES('{customer.FirstName}','{customer.LastName}','{customer.Email}','{customer.Password}',{customer.TelePhone},'{customer.CreatedDateTime}','{customer.ModifiedDateTime}')";
            using (SqlConnection sqlConnection = new SqlConnection(configuration.library.ConfigurationManager.Instance.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection)) //This really should go into a transcation
                {
                    return (int)sqlCommand.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the customers from the database.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private List<albelli.datacontract.Customer> GetCustomerDetails(int? customerId)
        {
            List<albelli.datacontract.Customer> customers = new List<datacontract.Customer>();
            string sqlQuery = "select customerid,firstname,lastname,email,password,telephone,createddate,modifieddate from customer";
            if (customerId.HasValue)
            {
                sqlQuery += $" Where customerid  = {customerId}";
            }
            using (SqlConnection sqlConnection = new SqlConnection(configuration.library.ConfigurationManager.Instance.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            albelli.datacontract.Customer customer = new datacontract.Customer();
                            customer.CustomerId = dataReader.GetInt32(dataReader.GetOrdinal("customerid"));
                            customer.FirstName = dataReader.GetString(dataReader.GetOrdinal("firstname"));
                            customer.LastName = dataReader.GetString(dataReader.GetOrdinal("lastname"));
                            customer.Email = dataReader.GetString(dataReader.GetOrdinal("email"));
                            customer.Password = !dataReader.IsDBNull(dataReader.GetOrdinal("password"))?dataReader.GetString(dataReader.GetOrdinal("password")):string.Empty;
                            customer.TelePhone = !dataReader.IsDBNull(dataReader.GetOrdinal("telephone")) ?dataReader.GetInt64(dataReader.GetOrdinal("telephone")):-1;
                            customer.CreatedDateTime = dataReader.GetDateTime(dataReader.GetOrdinal("createddate"));
                            customer.ModifiedDateTime = dataReader.GetDateTime(dataReader.GetOrdinal("modifieddate"));

                            customers.Add(customer);
                        }
                    }

                }
            }
            return customers;
        }

        #endregion
    }

}
