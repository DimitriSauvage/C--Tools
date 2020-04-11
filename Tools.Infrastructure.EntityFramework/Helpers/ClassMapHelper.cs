using Microsoft.EntityFrameworkCore;
using Tools.Infrastructure.EntityFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tools.Infrastructure.EntityFramework.Helpers
{
    public static class ClassMapHelper
    {
        /// <summary>
        /// Obtient toutes les classes qui sont déclarées comme étant des maps
        /// </summary>
        /// <param name="assemblies">Assemblies concernées par la recherche</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllMaps(IEnumerable<Assembly> assemblies)
        {
            //List<Type> types = new List<Type>();
            ////Parcours de toutes les assemblies pour récupérer tous les maps
            //foreach (var assembly in assemblies)
            //{
            //    foreach (var type in assembly.GetTypes())
            //    {
            //        if (!type.IsAbstract && !type.IsInterface)
            //        {
            //            //Récupération des interfaces du type
            //            Type[] myInterfaces = type.GetInterfaces();
            //            bool found = false;
            //            int index = 0;

            //            //Recherche du type voulu
            //            while (!found && index < myInterfaces.Length)
            //            {
            //                Type typeToTest = myInterfaces[index];
            //                //Recherche si l'interface est présente
            //                if (typeToTest.Name.Contains(typeof(IEntityTypeConfiguration<>).Name))
            //                {
            //                    types.Add(type);
            //                    found = true;
            //                }
            //                else
            //                {
            //                    index++;
            //                }
            //            }
            //        }
                    
            //    }

            //}
            return assemblies
                .SelectMany(ass => ass.GetTypes())
                .SelectMany(type => type.GetInterfaces())
                .Where(inter => inter.GetType().IsAssignableFrom(typeof(IEntityTypeConfiguration<>)));

            return assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x =>
                    typeof(EntityWithTrackingMap<>).IsAssignableFrom(x)
                    || typeof(EntityWithIdMap<>).IsAssignableFrom(x)
                    || typeof(EntityWithCompositeIdMap<>).IsAssignableFrom(x)
                    && !x.IsAbstract)
                .ToList();
        }
        
    }
}
