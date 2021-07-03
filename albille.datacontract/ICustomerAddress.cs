namespace albelli.datacontract
{
    public interface ICustomerAddress
    {
        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the customer address id.
        /// </summary>
        int CustomerAddressId { get; set; }
    }
}