using System;
using Microsoft.Extensions.DependencyInjection;

namespace DimitriSauvageTools.Helpers
{
    public class ServiceCollectionHelper
    {
        #region Fields

        /// <summary>
        /// Service collection which manage the DI
        /// </summary>
        private static IServiceCollection ServiceCollection { get; set; }

        /// <summary>
        /// Service provider to get the services
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
                return _serviceProvider = ServiceCollection.BuildServiceProvider();
            }
        }

        #endregion

        #region Constructor

        public ServiceCollectionHelper(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get an element from the dependency injection
        /// </summary>
        /// <typeparam name="TElement">Element type</typeparam>
        /// <returns>Element</returns>
        public static TElement GetElementFromDependencyInjection<TElement>()
        {
            return ServiceProvider.GetService<TElement>();
        }

        #endregion
    }
}