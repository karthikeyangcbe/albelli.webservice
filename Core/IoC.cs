using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace albelli.core
{
    public static class IoC
    {
        #region Fields

        /// <summary>
        /// The IoC container used to instantiate/inject object instances.
        /// </summary>
        private static IUnityContainer _Container = null;

        /// <summary>
        /// Synchronizes access to the singleton instance of <see cref="IUnityContainer"/>.
        /// </summary>
        private static object instanceSync = new object();

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes static members.
        /// </summary>
        static IoC()
        {
            System.Diagnostics.Trace.WriteLine("Ioc ctor");
        }

        #endregion

        #region Properties

        /// <summary>
        /// The IoC container used to instantiate/inject object instances.
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                IoC.EnsureIocContainer();
                return _Container;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates and configures the IoC container if it has not already been done.
        /// </summary>
        private static void EnsureIocContainer()
        {
            if (_Container == null)
            {
                lock (IoC.instanceSync)
                {
                    System.Diagnostics.Trace.WriteLine("Core.Ioc - create UnityContainer");
                    _Container = new UnityContainer();
                }
            }
        }
        #endregion
    }
}
