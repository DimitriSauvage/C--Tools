using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Tools.Helpers
{
    public static class TypeHelper
    {
        /// <summary>
        /// Permet de récupérer le premier type dont le nom correspond à celui passé en paramètre
        /// </summary>
        /// <param name="name">Nom du type à trouver</param>
        /// <param name="assemblies">Liste des assemblies dans lesquels chercher</param>
        /// <returns></returns>
        public static Type GetTypeFromName(string name, IEnumerable<Assembly> assemblies)
        {
            return GetTypesFromName(name, assemblies)?.FirstOrDefault();
        }

        /// <summary>
        /// Permet de récupérer la liste des types dont le nom correspond à celui passé en paramètre
        /// </summary>
        /// <param name="name">Nom du type à trouver</param>
        /// <param name="assemblies">Liste des assemblies dans lesquels chercher</param>
        /// <returns>Liste des types correspondant</returns>
        public static IEnumerable<Type> GetTypesFromName(string name, IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.Name == name)
                .ToList();
        }

        /// <summary>
        /// Vérifie si un type est une liste générique
        /// </summary>
        /// <param name="type">Type à vérifeir</param>
        /// <returns></returns>
        public static bool IsGenericList(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IList<>));
        }

        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <typeparam name="TBaseType">Base type</typeparam>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetImplementations<TBaseType>()
            where TBaseType : class
        {
            return GetImplementations(typeof(TBaseType));
        }

        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <typeparam name="TBaseType">Base type</typeparam>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetImplementations<TBaseType>(IEnumerable<Assembly> assemblies)
            where TBaseType : class
        {
            return GetImplementations(typeof(TBaseType), assemblies);
        }

        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="type">Base type</param>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetImplementations(Type type)
        {
            return GetImplementations(type, AppDomain.CurrentDomain.GetAssemblies());
        }


        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="type">Base type</param>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetImplementations(Type type, IEnumerable<Assembly> assemblies)
        {
            return GetImplementations(new List<Type>() {type}, assemblies);
        }

        /// <summary>
        /// Get the types implementing the types list
        /// </summary>
        /// <param name="types">Types</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetImplementations(IEnumerable<Type> types)
        {
            return GetImplementations(types, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Get the types implementing the types list
        /// </summary>
        /// <param name="types">Base types</param>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetImplementations(IEnumerable<Type> types,
            IEnumerable<Assembly> assemblies)
        {
            //Get all types
            return assemblies
                .SelectMany(x => x.GetTypes())
                .Where(type => types.Any(typeToFound =>
                    type.GetInterfaces().Any(x => x.GUID.Equals(typeToFound.GUID)) ||
                    type.GetAllBaseTypes().Any(x => x.GUID.Equals(typeToFound.GUID))))
                .ToList();
        }

        /// <summary>
        /// Get all type base types
        /// </summary>
        /// <param name="type">Type for which get the base types</param>
        /// <returns>All base types</returns>
        public static IEnumerable<Type> GetAllBaseTypes([NotNull] this Type type)
        {
            var result = new List<Type>();
            var baseType = type.BaseType;
            while (baseType != null)
            {
                result.Add(baseType);
                baseType = baseType.BaseType;
            }

            return result;
        }
    }
}