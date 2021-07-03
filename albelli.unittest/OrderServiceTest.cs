using NUnit.Framework;
using FakeItEasy;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using albelli.datacontract;
using System.Threading.Tasks;
using System;
using NUnit.Framework;

namespace albelli.unittest
{

    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            albelli.webservice.IoC.IocConfig.Instance.RegisterTypes(core.IoC.Container);
        }

        [Test]
        public void Test1()
        {
            //Arrange Fake DAL
            var productsDAL = A.Fake<albelli.DAL.IProductsDAL>();
            var ordersDAL = A.Fake<albelli.DAL.IOrderDAL>();

            //Arrange Fake DB objects
            var fakeConfigManager = A.Fake< configuration.library.IConfigurationManager>();
            A.CallTo(() => fakeConfigManager.ConnectionString).Returns(string.Empty);
            //var fakeSQLConnection = A.Fake<System.Data.SqlClient.SqlConnection>();
            //A.CallTo(()=>fakeSQLConnection.Open()).DoesNothing();
            //var fakeSQLCommand = A.Fake<System.Data.SqlClient.SqlCommand>();
            // Arrange fake Product details
            List<IProductDetails> productDetails = ArrangeProductsList();
            // Returns the fake product list when a call is made to products DAL
            A.CallTo(() => productsDAL.GetProductDetails()).Returns(productDetails);
            // Arranges the Order that needs to be passed to the controller.
            IOrder order = ArrangeOrders();

            //Arrange fake order details from DAL
            ArrangeOrderToReturnFromDAL(order);
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.OrderId, 1);

        }

        private void ArrangeOrderToReturnFromDAL(IOrder order)
        {
            order.OrderId = 1;
        }

        /// <summary>
        /// Arranges the Order <see cref="datacontract.IOrder"/>
        /// </summary>
        /// <returns></returns>
        private IOrder ArrangeOrders()
        {
            datacontract.IOrder order = new datacontract.Order();
            datacontract.IOrderDetails orderDetails = new datacontract.OrderDetails();

            order.CustomerId = 1;
            order.OrderId = 1;
            order.OrderDetails = new List<IOrderDetails>();
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.calendar, 4));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.mug, 4));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.canvas, 5));

            return order;
        }

        /// <summary>
        /// Arranges Order details <see cref="datacontract.IOrderDetails"/>
        /// </summary>
        /// <param name="productNames"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private IOrderDetails ArrangeOrderDetails(datacontract.ProductNames productNames,short quantity)
        {
            IOrderDetails orderDetails = new datacontract.OrderDetails();
            orderDetails.ProductsType = productNames;
            orderDetails.Quantity = quantity;
            return orderDetails;
        }

        /// <summary>
        /// Arranges product list <see cref="datacontract.IProductDetails"/>
        /// </summary>
        /// <returns></returns>
        private List<IProductDetails> ArrangeProductsList()
        {
            List<datacontract.IProductDetails> productDetails = new List<datacontract.IProductDetails>();
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.photoBook, 19.0M, 1));
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.calendar, 10.0M, 1));
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.canvas, 16.0M, 1));
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.greetingcards, 4.7M, 1));
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.mug, 94.0M, 1));
            return productDetails;
        }

        /// <summary>
        /// Arranges by creating a new product detail instance.
        /// </summary>
        /// <param name="ProductId"><
        /// The product id that needs to be added to the product detail.
        /// /param>
        /// <param name="productName">
        /// The product name that needs to be added to the product detail.
        /// </param>
        /// <param name="ProductDimensionInMM">
        /// The product dimension that needs to be added to the product detail.
        /// </param>
        /// <param name="StackableUpto">
        /// The stackable upto that needs to be added to the product detail.
        /// </param>
        /// <returns></returns>
        private datacontract.IProductDetails ArrangeProudcts(short ProductId, datacontract.ProductNames productName,decimal ProductDimensionInMM, short StackableUpto)
        {
            return new datacontract.ProductDetails() { ProductId = ProductId, ProductName = productName, ProductDimensionInMM = ProductDimensionInMM, StackableUpto = StackableUpto };
        }
    }
}