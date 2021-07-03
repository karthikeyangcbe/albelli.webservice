namespace albelli.datacontract
{
    public interface IProductDetails
    {
        /// <summary>
        /// Gets or sets the product dimension in MM.
        /// </summary>
        decimal ProductDimensionInMM { get; set; }

        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        short ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.<see cref="ProductName"/>
        /// </summary>
        ProductNames ProductName { get; set; }

        /// <summary>
        /// Gets or sets how many items this product can be stacked upto. Returns 0 if its a product is not stackable.
        /// </summary>
        short StackableUpto { get; set; }
    }
}