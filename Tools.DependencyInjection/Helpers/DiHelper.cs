using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tools.Application.Abstractions;
using Tools.Helpers;

namespace Tools.DependencyInjection.Helpers
{
    /// <summary>
    /// Helper for the dependency injection
    /// </summary>
    public static class DiHelper
    {
        /// <summary>
        /// Add all services in the DI
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        /// <param name="assemblies">Assemblies to browse</param>
        public static void AddAllServices(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            //Get all types
            var services = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(type => !type.IsAbstract && type.GetAllBaseTypes().Any(x =>
                                   x.GUID.Equals(typeof(BaseService).GUID)
                                   || x.GUID.Equals(typeof(BaseService<,>)
                                       .GUID)))
                .ToList();

            //Add to the DI
            foreach (var service in services)
            {
                serviceCollection.AddScoped(service);
            }
        }
    }
}