using NUnit.Framework;
using FakeItEasy;
using albelli.datacontract;
using System.Collections.Generic;
using Unity;
using albelli.DAL;

namespace albelli.test
{
    /// <summary>
    /// This class contains all the tests to validate the Order Service.
    /// </summary>
    public class OrderTest
    {
        #region Setup Method
        
        /// <summary>
        /// Register the types for IoC
        /// </summary>
        [SetUp]
        public void Setup()
        {
            albelli.webservice.IoC.IocConfig.Instance.RegisterTypes(core.IoC.Container);

        }
        #endregion

        #region Test Methods

        /// <summary>
        /// This method tests if a OrderId is returned in the response.
        /// </summary>
        [Test]
        public void TestOrderIdIsReturned()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            //Arrange Orders
            order = ArrangeOrders();
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.OrderId, 1);

        }

        /// <summary>
        /// This method tests if the CreatedDate is today.
        /// </summary>
        [Test]
        public void TestOrderCreatedDateIsToday()
        {
            //Arrange 
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrders();

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.CreatedDateTime.Date, System.DateTime.Today.Date);

        }

        /// <summary>
        /// This method tests the width required for bin by creating a order with 
        /// 2 photobooks and 2 canvas and 4 mugs(just to ensure the less than or Equals is staisfied).
        /// </summary>
        [Test]
        public void TestOrderWidthForBin_2PhotoBook_2Canvas_4Mugs()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);
            
            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith2Photobook2CanvsAndXMugs(4);

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.RequiredBinWidth, 164M);//(2*19)+(2*19)+(1*94)=164

        }

        /// <summary>
        /// This test is ensure photobooks and calenders are not stackable
        /// </summary>
        [Test]
        public void TestOrderWidthForBin_5PhotoBook_4Calender_2Mugs()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith2Photobook2CanvsAndXMugs(2);
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.RequiredBinWidth, 164M);//(2*19)+(2*10)+(1*94)=164

        }

        /// <summary>
        /// This method tests to calcualte the width for the bin by passing more than 1 stackable unit (which is 5 and > 4)
        /// </summary>
        [Test]
        public void TestOrderWidthForBin_1PhotoBook_2Calender_5Mugs()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith2Photobook2CanvsAndXMugs(5);

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.RequiredBinWidth, 258M);//(2*19)+(2*16)+(2*94)=258

        }

        /// <summary>
        /// This test is to ensure when the mod is 0 and quotient is > 0. 
        /// For example When 2 stackable unit is passed which is 8 mugs
        /// </summary>
        [Test]
        public void TestOrderWidthForBin_1PhotoBook_2Calender_8Mugs()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith2Photobook2CanvsAndXMugs(8);

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.RequiredBinWidth, 258M);//(2*19)+(2*16)+(2*94)=258

        }

        /// <summary>
        /// This tests my passing the maximum mugs (Int16.MaxValue)
        /// </summary>
        [Test]
        public void TestOrderWidthForBin_1PhotoBook_2Calender_Int16MaxValueMugs()//This test is to ensure when the mod is 3 and quotient is > 0
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith2Photobook2CanvsAndXMugs(System.Int16.MaxValue);

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();
            var result = orderService.Create(order);

            //Assert
            Assert.AreEqual(result.RequiredBinWidth, 770118M);//(2*19)+(2*16)+(8192*94)=770118

        }

        /// <summary>
        /// This test is to expect a exeception when 0 quantity is passed.
        /// </summary>
        [Test]
        public void TestOrder_With0Quantity()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderswith0Photobook2CanvsAndXMugs(2);

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();

            //Assert
            Assert.Throws<System.ArgumentNullException>(() => orderService.Create(order));
        }

        /// <summary>
        /// This test is to validate if an exeception is raised when order id is passed as 0.
        /// </summary>
        [Test]
        public void TestOrder_WithOrderIdAs0()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeOrderIDAsZero();

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();

            //Assert
            Assert.Throws<System.ArgumentNullException>(() => orderService.Create(order));
        }

        /// <summary>
        /// This test is to validate if a exception is thrown when Customer id is passed as 0. 
        /// This (Related to customer id being mandatory) is not mentioned in the task but added addtional validation.
        /// </summary>
        [Test]
        public void TestOrder_WithCustomerIDAsZero()
        {
            //Arrange Fake DAL
            IOrderDAL ordersDAL;
            IOrder order;
            ArrangeFakeDALAndDataObjects(out ordersDAL);

            // Arranges the Order that needs to be passed to the controller.
            order = ArrangeCustomerIDAsZero();

            //Arrange fake order details from DAL
            A.CallTo(() => ordersDAL.AddNewOrder(order)).Returns(order);

            //Act
            var orderService = new albelli.servicelibrary.OrderService();

            //Assert
            Assert.Throws<System.ArgumentNullException>(() => orderService.Create(order));
        }

        #endregion

        #region Arrange Methods

        /// <summary>
        /// Arranges Fake DAL and all other Data objects that's needed.
        /// </summary>
        /// <param name="ordersDAL"></param>
        private void ArrangeFakeDALAndDataObjects(out IOrderDAL ordersDAL)
        {
            IProductsDAL productsDAL;

            ArrangeFakeDAL(out productsDAL, out ordersDAL);

            //Arrange Fake DB objects
            var fakeConfigManager = A.Fake<configuration.library.IConfigurationManager>();
            A.CallTo(() => fakeConfigManager.ConnectionString).Returns(string.Empty);
            // Arrange fake Product details
            List<IProductDetails> productDetails = ArrangeProductsList();
            // Returns the fake product list when a call is made to products DAL
            A.CallTo(() => productsDAL.GetProductDetails()).Returns(productDetails);
            // Arranges the Order that needs to be passed to the controller.
            //Arrange fake order details from DAL
        }

        /// <summary>
        /// Arranges Fake DAL.
        /// </summary>
        /// <param name="productsDAL"></param>
        /// <param name="ordersDAL"></param>
        private void ArrangeFakeDAL(out IProductsDAL productsDAL, out IOrderDAL ordersDAL)
        {
            productsDAL = A.Fake<albelli.DAL.IProductsDAL>();
            ordersDAL = A.Fake<albelli.DAL.IOrderDAL>();
            core.IoC.Container.RegisterInstance(productsDAL);
            core.IoC.Container.RegisterInstance(ordersDAL);
        }

        /// <summary>
        /// Arranges the Order <see cref="datacontract.IOrder"/>
        /// </summary>
        /// <returns></returns>
        private IOrder ArrangeOrders()
        {
           return ArrangeOrderswith2Photobook2CanvsAndXMugs(2);
        }

        /// <summary>
        /// Arranges orders with 2 photoBook,2 canvas and X mugs(This is passed as param).
        /// </summary>
        /// <param name="mugCount">
        /// The number of mug count to pass.
        /// </param>
        /// <returns>
        /// The arranged order object.
        /// </returns>
        private IOrder ArrangeOrderswith2Photobook2CanvsAndXMugs(short mugCount)
        {
            datacontract.IOrder order = new datacontract.Order();
            datacontract.IOrderDetails orderDetails = new datacontract.OrderDetails();

            order.CustomerId = 1;
            order.OrderId = 1;
            order.CreatedDateTime = System.DateTime.Now;
            order.OrderDetails = new List<IOrderDetails>();
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.photoBook, 2));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.mug, mugCount));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.canvas, 2));
            return order;
        }

        /// <summary>
        /// Arranges orders with 0 photoBook,2 canvas and X mugs(This is passed as param).
        /// </summary>
        /// <param name="mugCount">
        /// The number of mug count to pass.
        /// </param>
        /// <returns>
        /// The arranged order object.
        /// </returns>
        private IOrder ArrangeOrderswith0Photobook2CanvsAndXMugs(short mugCount)
        {
            datacontract.IOrder order = new datacontract.Order();
            datacontract.IOrderDetails orderDetails = new datacontract.OrderDetails();

            order.CustomerId = 1;
            order.OrderId = 1;
            order.CreatedDateTime = System.DateTime.Now;
            order.OrderDetails = new List<IOrderDetails>();
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.photoBook, 0));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.mug, mugCount));
            order.OrderDetails.Add(ArrangeOrderDetails(ProductNames.canvas, 2));
            return order;
        }

        /// <summary>
        /// Arranges the order by having the order id as 0.
        /// </summary>
        /// <returns>
        /// The arranged order object.
        /// </returns>
        private IOrder ArrangeOrderIDAsZero()
        {
            datacontract.IOrder order = new datacontract.Order();
            datacontract.IOrderDetails orderDetails = new datacontract.OrderDetails();

            order.CustomerId = 1;
            order.OrderId = 0;
            order.CreatedDateTime = System.DateTime.Now;
            return order;
        }

        /// <summary>
        /// Arranges the order by having customer id as 0
        /// </summary>
        /// <returns>
        /// The arranged order object.
        /// </returns>
        private IOrder ArrangeCustomerIDAsZero()
        {
            datacontract.IOrder order = new datacontract.Order();
            datacontract.IOrderDetails orderDetails = new datacontract.OrderDetails();

            order.CustomerId = 0;
            order.OrderId = 0;
            order.CreatedDateTime = System.DateTime.Now;
            return order;
        }

        /// <summary>
        /// Arranges Order details <see cref="datacontract.IOrderDetails"/>
        /// </summary>
        /// <param name="productNames"></param>
        /// <param name="quantity"></param>
        /// <returns>
        /// The arranged order object.
        /// </returns>
        private IOrderDetails ArrangeOrderDetails(datacontract.ProductNames productNames, short quantity)
        {
            IOrderDetails orderDetails = new datacontract.OrderDetails();
            orderDetails.ProductsType = productNames;
            orderDetails.Quantity = quantity;
            return orderDetails;
        }

        /// <summary>
        /// Arranges product list <see cref="datacontract.IProductDetails"/>
        /// </summary>
        /// <returns>
        /// The list of products.
        /// </returns>
        private List<IProductDetails> ArrangeProductsList()
        {
            List<datacontract.IProductDetails> productDetails = new List<datacontract.IProductDetails>();
            productDetails.Add(ArrangeProudcts(1, datacontract.ProductNames.photoBook, 19.0M, 0));
            productDetails.Add(ArrangeProudcts(2, datacontract.ProductNames.calendar, 10.0M, 0));
            productDetails.Add(ArrangeProudcts(3, datacontract.ProductNames.canvas, 16.0M, 0));
            productDetails.Add(ArrangeProudcts(4, datacontract.ProductNames.greetingcards, 4.7M, 0));
            productDetails.Add(ArrangeProudcts(5, datacontract.ProductNames.mug, 94.0M, 4));
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
        /// <returns>
        /// The product list.
        /// </returns>
        private datacontract.IProductDetails ArrangeProudcts(short ProductId, datacontract.ProductNames productName, decimal ProductDimensionInMM, short StackableUpto)
        {
            return new datacontract.ProductDetails() { ProductId = ProductId, ProductName = productName, ProductDimensionInMM = ProductDimensionInMM, StackableUpto = StackableUpto };
        }

        #endregion
    }
}