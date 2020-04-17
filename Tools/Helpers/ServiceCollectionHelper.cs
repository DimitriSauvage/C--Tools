using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.Helpers
{
    public class ServiceCollectionHelper
    {
        #region Fields

        /// <summary>
        /// Service collection
        /// </summary>
        private static IServiceProvider _serviceProvider = null;

        /// <summary>
        /// Service provider getter
        /// </summary>
        private static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider != null) return _serviceProvider;

                //Get the service collection
                _serviceProvider = new ServiceCollection().BuildServiceProvider();

                return _serviceProvider;
            }
        }

        #endregion

        /// <summary>
        /// Get an element from the dependency injection
        /// </summary>
        /// <typeparam name="TElement">Element type</typeparam>
        /// <returns>Element</returns>
        public static TElement GetElementFromDependencyInjection<TElement>()
        {
            return ServiceProvider.GetService<TElement>();
        }
    }
}