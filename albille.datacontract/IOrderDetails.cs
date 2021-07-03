namespace albelli.datacontract
{
    public interface IOrderDetails
    {
        /// <summary>
        /// Gets or sets the order details id.
        /// </summary>
        int OrderDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the product type.
        /// </summary>
        ProductNames ProductsType { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        short Quantity { get; set; }
    }
}