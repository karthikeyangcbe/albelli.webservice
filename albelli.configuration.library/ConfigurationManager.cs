using System;

namespace albelli.configuration.library
{
    /// <summary>
    /// All the configuration details will be read from this class.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        #region Fields
        private static ConfigurationManager configurationManager = null;
        private static object instanceSync = new object();
        #endregion

        #region Properties
        public static ConfigurationManager Instance
        {
            get
            {
                if (configurationManager == null)
                {
                    lock (instanceSync)
                    {
                        configurationManager = new ConfigurationManager();
                    }
                }
                return configurationManager;
            }
        }
        #endregion
        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                 return "Server=KGENGARAM-5410;User ID=sa;Password=Aspect123$;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;database=albelli";
                //return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
        }
    }
}
