using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DimitriSauvageTools.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DimitriSauvageTools.DependencyInjection.Helpers
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
        /// <param name="baseServiceTypes">Base types which are inherited or implemented by services</param>
        public static void AddAllServices(this IServiceCollection serviceCollection,
            IEnumerable<Assembly> assemblies,
            IEnumerable<Type> baseServiceTypes)
        {
            //Get all types
            assemblies
                .SelectMany(x => x.GetTypes()) //Get all types
                .Where(type =>
                    !(type.IsAbstract || type.IsInterface) //Get not abstract class
                    && (type
                            .GetAllBaseTypes()
                            .Any(baseType => baseServiceTypes.Any(x => x.GUID == baseType.GUID))
                        ||
                        type
                            .GetInterfaces()
                            .Any(baseInterface => baseServiceTypes.Any(x => x.GUID == baseInterface.GUID))))
                .ToList()
                .ForEach(x => serviceCollection.AddScoped(x)); //Add services to DI
        }
    }
}