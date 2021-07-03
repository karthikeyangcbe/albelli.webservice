using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace albelli.core
{
    public interface IIoCConfig
    {

        #region Methods

        /// <summary>
        /// Registers the types within the assembly within the specified IoC container.
        /// </summary>
        /// <param name="container">
        /// The IoC container with which to register assembly types.
        /// </param>
        void RegisterTypes(IUnityContainer container);

        #endregion
    }
}
