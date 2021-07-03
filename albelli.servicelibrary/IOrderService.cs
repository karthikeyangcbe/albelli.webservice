using albelli.datacontract;

namespace albelli.servicelibrary
{
    /// <summary>
    /// Contains all the details about the order service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>
        /// Returns the order details along with Width required to store.
        /// </returns>
        IOrder Create(IOrder order);

        /// <summary>
        /// Gets the order details for the order id requested.
        /// </summary>
        /// <param name="orderId">
        /// The order id for which the order is requested.
        /// </param>
        /// <returns>
        /// The order <see cref="datacontract.IOrder"/>.
        /// </returns>
        datacontract.IOrder GetOrder(int orderId);

        /// <summary>
        /// Gets the width required for bin.
        /// </summary>
        /// <param name="orderId">
        /// The order id for which we need the width for the bin.
        /// </param>
        /// <returns>
        /// Returns a value indicating the width required for the bin.
        /// </returns>
        decimal GetWidthForBin(int orderId);
    }
}