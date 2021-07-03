using System;

namespace albelli.datacontract
{
    /// <summary>
    /// Creates a instance of the product details.
    /// </summary>
    public class ProductDetails : IProductDetails
    {
        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        public short ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product name.<see cref="ProductName"/>
        /// </summary>
        public ProductNames ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product dimension in MM.
        /// </summary>
        public decimal ProductDimensionInMM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how many items this product can be stacked upto. Returns 0 if its a product is not stackable.
        /// </summary>
        public short StackableUpto
        {
            get;
            set;
        }
    }
}
