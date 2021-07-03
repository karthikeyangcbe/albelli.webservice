using Unity;

namespace albelli.webservice.IoC
{
    public class IocConfig : core.IIoCConfig
    {
        #region Fields
        private static IocConfig ioCConfig = null;
        private static object instanceSync = new object();
        #endregion

        #region Constructor
        private IocConfig()
        {
        }
        #endregion

        #region Properties
        public static IocConfig Instance
        {
            get
            {
                if (ioCConfig == null)
                {
                    lock (instanceSync)
                    {
                        ioCConfig = new IocConfig();
                    }
                }
                return ioCConfig;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Registers all the types that are needed.
        /// </summary>
        /// <param name="container">
        /// The Unity container.
        /// </param>
        public void RegisterTypes(IUnityContainer container)
        {
            this.RegisterDataContracts(container);
            this.RegisterService(container);
            this.RegisterConfiguration(container);
            this.RegisterEncryption(container);
            this.RegisterDAL(container);

        }

        /// <summary>
        /// Registers the data contracts
        /// </summary>
        /// <param name="unityContainer">
        /// The Unity container.
        /// </param>
        private void RegisterDataContracts(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<datacontract.ICustomer, datacontract.Customer>();
            unityContainer.RegisterType<datacontract.ICustomerAddress, datacontract.CustomerAddress>();
            unityContainer.RegisterType<datacontract.IOrder, datacontract.Order>();
            unityContainer.RegisterType<datacontract.IOrderDetails, datacontract.IOrderDetails>();
            unityContainer.RegisterType<datacontract.IProductDetails, datacontract.IProductDetails>();
        }

        /// <summary>
        /// Registers the Services
        /// </summary>
        /// <param name="unityContainer">
        /// The Unity container.
        /// </param>
        private void RegisterService(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<servicelibrary.ICustomerService, servicelibrary.CustomerService>();
            unityContainer.RegisterType<servicelibrary.IOrderService, servicelibrary.OrderService>();
        }

        /// <summary>
        /// Registers the classes for Configuration.
        /// </summary>
        /// <param name="unityContainer">
        /// The Unity container.
        /// </param>
        private void RegisterConfiguration(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<configuration.library.IConfigurationManager, configuration.library.ConfigurationManager>();
        }

        /// <summary>
        /// Registers the encryption classes.
        /// </summary>
        /// <param name="unityContainer">
        /// The Unity container.
        /// </param>
        private void RegisterEncryption(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<encryption.utility.ICryptoUtility, encryption.utility.CryptoUtility>();
        }

        /// <summary>
        /// Registers the DAL classes.
        /// </summary>
        /// <param name="unityContainer">
        /// The Unity container.
        /// </param>
        private void RegisterDAL(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<DAL.IProductsDAL, DAL.ProductsDAL>();
            unityContainer.RegisterType<DAL.IOrderDAL, DAL.OrderDAL>();
            unityContainer.RegisterType<DAL.ICustomerDAL, DAL.CustomerDAL>();
        }


        #endregion
    }
}
