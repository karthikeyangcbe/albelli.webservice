using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace albelli.DAL
{
    /// <summary>
    /// Data access class for Products.
    /// </summary>
    public class ProductsDAL : IProductsDAL
    {
        /// <summary>
        /// Gets all the details from the products table.
        /// </summary>
        /// <returns>
        /// Returns the list of products from the database
        /// </returns>
        public List<datacontract.IProductDetails> GetProductDetails()
        {
            List<albelli.datacontract.IProductDetails> products = new List<datacontract.IProductDetails>();
            string sqlQuery = "SELECT [productid],[productname],[productdimensioninmm],[stackableupto] FROM [dbo].[productdetails]";
            using (SqlConnection sqlConnection = new SqlConnection(configuration.library.ConfigurationManager.Instance.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            albelli.datacontract.IProductDetails productDetails = new datacontract.ProductDetails();
                            productDetails.ProductId = dataReader.GetInt16(dataReader.GetOrdinal("productid"));
                            productDetails.ProductName = (datacontract.ProductNames)productDetails.ProductId;
                            productDetails.ProductDimensionInMM = dataReader.GetDecimal(dataReader.GetOrdinal("productdimensioninmm"));
                            productDetails.StackableUpto = dataReader.GetInt16(dataReader.GetOrdinal("stackableupto"));

                            products.Add(productDetails);
                        }
                    }

                }
            }
            return products;
        }
    }
}
