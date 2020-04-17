using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Tools.Infrastructure.EntityFramework.Helpers
{
    public static class MapHelper
    {
        /// <summary>
        /// Obtient toutes les classes qui sont déclarées comme étant des maps
        /// </summary>
        /// <param name="assemblies">Assemblies concernées par la recherche</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllMaps(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(ass => ass.GetTypes())
                .SelectMany(type => type.GetInterfaces())
                .Where(inter => inter.GetType().IsAssignableFrom(typeof(IEntityTypeConfiguration<>)));
        }

        /// <summary>
        /// Get all maps in the current assembly
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllMaps()
        {
            return GetAllMaps(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Apply all maps in the assemblies to the model builder
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        /// <param name="modelBuilder">Model builder</param>
        public static void ApplyMapsConfiguration(IEnumerable<Assembly> assemblies, ModelBuilder modelBuilder)
        {
            var maps = GetAllMaps(assemblies);

            foreach (var map in maps)
            {
                dynamic mapInstance = Activator.CreateInstance(map);
                modelBuilder.ApplyConfiguration(mapInstance);
            }
        }

        /// <summary>
        /// Apply all maps in the assemblies to the model builder
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        public static void ApplyMapsConfiguration(ModelBuilder modelBuilder)
        {
            ApplyMapsConfiguration(AppDomain.CurrentDomain.GetAssemblies(), modelBuilder);
        }
    }
}
