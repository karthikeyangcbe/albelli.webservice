using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace albelli.servicelibrary
{
    /// <summary>
    /// Has all the operations related to Order.
    /// </summary>
    public class OrderService : IOrderService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Public Methods
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">
        /// Details of the order<see cref="datacontract.IOrder"/> to create.
        /// </param>
        /// <returns>
        /// The order <see cref="Create(datacontract.IOrder)"/> details along with minium width required for bin
        /// </returns>
        public datacontract.IOrder Create(datacontract.IOrder order)
        {
            try
            {
                log.Info($"Entering '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
                // Validate he customer object first before updating it.
                if (this.Validate(order))
                {
                    //Creates the DAL for Order.
                    albelli.DAL.IOrderDAL orderDAL = core.IoC.Container.Resolve<albelli.DAL.IOrderDAL>();
                    //Creates the DAL for Products
                    albelli.DAL.IProductsDAL productsDAL = core.IoC.Container.Resolve<albelli.DAL.IProductsDAL>();

                    // Adds the order received into the database
                    datacontract.IOrder updatedOrder = orderDAL.AddNewOrder(order);

                    //Gets the prdouct details from the lookup table.
                    //Note: Potentially this could be cached so we avoid DB calls to get the product details everytime we add a new order.
                    List<datacontract.IProductDetails> productDetails = productsDAL.GetProductDetails();
                    //Calculate the width and update it
                    updatedOrder.RequiredBinWidth = this.CalculateWidth(productDetails, updatedOrder);
                }

            }
            catch (Exception ex)
            {
                log.Error($"Error in '{System.Reflection.MethodBase.GetCurrentMethod().Name}'", ex);
                throw;
            }
            finally
            {
                log.Info($"Exiting '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
            }

            return order;
        }

        /// <summary>
        /// Gets the order details.
        /// </summary>
        /// <param name="orderId">
        /// The order id for which the order needs to be returned.
        /// </param>
        /// <returns>
        /// The order <see cref="datacontract.Order"/> details.
        /// </returns>
        public datacontract.IOrder GetOrder(int orderId)
        {
            datacontract.IOrder order = null;
            try
            {
                log.Info($"Entering '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
                order = GetOrders(orderId);
            }
            catch(Exception ex)
            {
                log.Error($"Error in '{System.Reflection.MethodBase.GetCurrentMethod().Name}'", ex);
                throw;
            }
            finally
            {
                log.Info($"Exiting '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
            }
            return order;
        }

        /// <summary>
        /// Gets the width required for bin.
        /// </summary>
        /// <param name="orderId">
        /// The order id for which we need the width for the bin.
        /// </param>
        /// <returns>
        /// Returns a value indicating the width required for the bin.
        /// </returns>
        public decimal GetWidthForBin(int orderId)
        {
            datacontract.IOrder order = null;
            try
            {
                log.Info($"Entering '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
                order = GetOrders(orderId);
            }
            catch(Exception ex)
            {
                log.Error($"Error in '{System.Reflection.MethodBase.GetCurrentMethod().Name}'", ex);
                throw;
            }
            finally
            {
                log.Info($"Exiting '{System.Reflection.MethodBase.GetCurrentMethod().Name}'");
            }
            return order.RequiredBinWidth;
        }

        private datacontract.IOrder GetOrders(int orderId)
        {
            datacontract.IOrder order;
            if (orderId <= 0)
                throw new ArgumentNullException("Invalid Order Id");
            //Creates the DAL for Order.
            albelli.DAL.IOrderDAL orderDAL = core.IoC.Container.Resolve<albelli.DAL.IOrderDAL>();
            //Creates the DAL for Products
            albelli.DAL.IProductsDAL productsDAL = core.IoC.Container.Resolve<albelli.DAL.IProductsDAL>();
            //Gets the order details from the database
            order = orderDAL.GetOrderDetails(orderId);
            if (order == null)
                throw new ArgumentOutOfRangeException("Invalid Order Id");
            //Gets the prdouct details from the lookup table.
            //Note: Potentially this could be cached so we avoid DB calls to get the product details everytime we add a new order.
            List<datacontract.IProductDetails> productDetails = productsDAL.GetProductDetails();
            //Calculate the width and update it
            order.RequiredBinWidth = this.CalculateWidth(productDetails, order);
            return order;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calcualtes the Minimum width for bin
        /// </summary>
        /// <param name="productdetails">
        /// The product details <see cref="datacontract.IProductDetails"/>.
        /// The information on the size required for each product type is stored in database in a column.
        /// The information on how many items can be stacked for a product type is also stored in the database.
        /// </param>
        /// <param name="order"></param>
        /// <returns></returns>
        private decimal CalculateWidth(List<datacontract.IProductDetails> productdetails, datacontract.IOrder order)
        {
            decimal returnValue = 0;

            // Loop threugh the order details made by the customer.
            foreach (var item in order.OrderDetails)
            {
                //Get product details from the productdetails (which is lookup  containing all the product types from database)
                //by matching with the product id with the product type (enum) sent in the request
                var product = productdetails.FirstOrDefault(x => x.ProductId == (short)item.ProductsType);
                if (product != null)
                {
                    //If the particualr product is stackable
                    //if its not stackable we will have the value as 0 in the database)
                    //If its stackable we will have the value as greter than 0.
                    //For example mug will have a value as 4 and caleder will have a value as 0
                    //This is read from the database to provide the extendabiltiy without making a change when a new product is added 
                    //When a product is added by albelli all we need to add that to the product details table by providing the size and how much its stackable upto
                    if (product.StackableUpto > 0)
                    {
                        //If its stackable then multiply product dimension into stackableupto and then multiply it with quantilty of what customer ordered.
                        //For example: if 3 mugs are ordered then and if its less then the stackable unit size then we consider only 1 unit of stackable
                        // which is nothing but the product dimension in MM
                        if (item.Quantity <= product.StackableUpto)
                        {
                            returnValue += product.ProductDimensionInMM;
                        }
                        else
                        {
                            //If the ordered quantity is more than 1 stackable unit
                            //For example if the customer ordres 5 mugs where 4 mugs make 1 unit of stackable unit
                            //We need to calcuate the mod. For example if 5 mugs are ordered then 5%4(Stackable unit) then we get the mod of 1
                            //We also need to calcuate the quotient and in this example 5/4=1
                            int mod = item.Quantity % product.StackableUpto;
                            int quotient = item.Quantity / product.StackableUpto;
                            if (mod==0)//if the mod is 0 then we know quantity is in multiples of StackableUpto so we just need to multiply qty * quotient
                            {
                                returnValue += product.ProductDimensionInMM * quotient;
                            }
                            else // If not we know the mod is always going to less then 1 stackable (mod will be less than 4) so we need to multiply dimension*quotient+1 unit which is for the mod(<4 makes 1 stackable unit)
                            {
                                returnValue += (product.ProductDimensionInMM * quotient) + product.ProductDimensionInMM;
                            }
                        }
                    }
                    else
                    {
                        //If its not stackable then simply multiply the quantity of the item * product dimension
                        returnValue += product.ProductDimensionInMM * item.Quantity;
                    }
                }
            }

            return returnValue;

        }

        /// <summary>
        /// Validates the input from the order <see cref="datacontract.IOrder"/> object
        /// </summary>
        /// <param name="order">
        /// The order<see cref="datacontract.IOrder"/> to be validated.
        /// </param>
        /// <returns>
        /// True if its a valid object to save to the database.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If order is null
        /// If customerId is less than or equals 0
        /// If OrderId is less than or equals 0
        /// If ordered quanity is less than or equals to 0
        /// </exception>
        private bool Validate(datacontract.IOrder order)
        {
            bool returnValue = true;

            if (order == null)
                throw new ArgumentNullException("Invalid Order object");
            if (order.CustomerId <= 0)
                throw new ArgumentNullException("Invalid Customer ID");
            if (order.OrderId <= 0)
                throw new ArgumentNullException("Invalid Order id");
            if (order.OrderDetails.Any(x => x.Quantity <= 0))
                throw new ArgumentNullException("Quantity cannot be 0");

            return returnValue;
        }
        #endregion
    }
}
