using albelli.datacontract;
using System.Collections.Generic;

namespace albelli.DAL
{
    /// <summary>
    /// Data access class for Products.
    /// </summary>
    public interface IProductsDAL
    {
        /// <summary>
        /// Gets all the details from the products table.
        /// </summary>
        /// <returns>
        /// Returns the list of products from the database
        /// </returns>
        List<IProductDetails> GetProductDetails();
    }
}