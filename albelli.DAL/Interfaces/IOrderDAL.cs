using albelli.datacontract;

namespace albelli.DAL
{
    public interface IOrderDAL
    {
        /// <summary>
        /// Adds a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        IOrder AddNewOrder(IOrder order);
        /// <summary>
        /// Gets an existing order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IOrder GetOrderDetails(int orderId);
    }
}