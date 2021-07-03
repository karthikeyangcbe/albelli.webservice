using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace albelli.DAL
{
    public class OrderDAL : IOrderDAL
    {
        public albelli.datacontract.IOrder AddNewOrder(albelli.datacontract.IOrder order)
        {
            string sqlQuery = $"INSERT INTO [dbo].[order] ([customerid],[createddatetime]) output INSERTED.orderid VALUES ({order.CustomerId},'{DateTime.Now.ToString()}')";
            using (SqlConnection sqlConnection = new SqlConnection(configuration.library.ConfigurationManager.Instance.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection)) //This really should go into a transcation
                {
                    int id = (int)sqlCommand.ExecuteScalar();
                    order.OrderId = id;
                    foreach (var item in order.OrderDetails)
                    {

                        sqlQuery = $"INSERT INTO [dbo].[orderdetails] ([orderid],[producttypeid],[quantity]) VALUES ( {id},{(short)item.ProductsType},{item.Quantity})";
                        sqlCommand.CommandText = sqlQuery;
                        sqlCommand.ExecuteNonQuery();
                    }

                }
            }
            return order;
        }

        /// <summary>
        /// Gets an order
        /// </summary>
        /// <param name="orderId">
        /// The order id to get.
        /// </param>
        /// <returns>
        /// The order<see cref="datacontract.Order"/>
        /// </returns>
        public datacontract.IOrder GetOrderDetails(int orderId)
        {
            albelli.datacontract.IOrder order = null;
            string sqlQuery = $"SELECT [orderid],[customerid],[createddatetime]  FROM [dbo].[order] where [orderid] = {orderId}";
            using (SqlConnection sqlConnection = new SqlConnection(configuration.library.ConfigurationManager.Instance.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            order = new albelli.datacontract.Order();
                            order.OrderId = orderId;
                            order.CreatedDateTime = dataReader.GetDateTime(dataReader.GetOrdinal("createddatetime"));
                            order.CustomerId = dataReader.GetInt32(dataReader.GetOrdinal("customerid"));
                        }
                    }
                    if (order != null)
                    {
                        sqlCommand.CommandText = $"SELECT [orderdetailsid],[orderid],[producttypeid],[quantity]  FROM [dbo].[orderdetails] where [orderid] = {orderId}";
                        using (IDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            order.OrderDetails = new List<datacontract.IOrderDetails>();
                            while (dataReader.Read())
                            {
                                datacontract.OrderDetails orderDetails = new albelli.datacontract.OrderDetails();
                                orderDetails.OrderId = orderId;
                                orderDetails.OrderDetailsId = dataReader.GetInt32(dataReader.GetOrdinal("orderdetailsid"));
                                orderDetails.ProductsType = (datacontract.ProductNames)dataReader.GetInt32(dataReader.GetOrdinal("producttypeid"));
                                orderDetails.Quantity = dataReader.GetInt16(dataReader.GetOrdinal("quantity"));
                                order.OrderDetails.Add(orderDetails);
                            }
                        }
                    }
                }
            }
            return order;


        }
    }
}
