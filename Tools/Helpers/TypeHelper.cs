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
        public static IEnumerable<Type> GetBaseTypeImplementation<TBaseType>()
            where TBaseType : class
        {
            return GetBaseTypeImplementation(typeof(TBaseType));
        }

        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <typeparam name="TBaseType">Base type</typeparam>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetBaseTypeImplementation<TBaseType>(IEnumerable<Assembly> assemblies)
            where TBaseType : class
        {
            return GetBaseTypeImplementation(typeof(TBaseType), assemblies);
        }

        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="type">Base type</param>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetBaseTypeImplementation(Type type)
        {
            return GetBaseTypeImplementation(type, AppDomain.CurrentDomain.GetAssemblies());
        }


        /// <summary>
        /// Get the implementations of a base type
        /// </summary>
        /// <param name="type">Base type</param>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <returns>Implementations</returns>
        public static IEnumerable<Type> GetBaseTypeImplementation(Type type, IEnumerable<Assembly> assemblies)
        {
            if (!type.IsClass) throw new ArgumentException("The argument is not a class");
            return GetBaseTypeImplementation(new List<Type>() {type}, assemblies);
        }

        /// <summary>
        /// Get the types implementing the types list
        /// </summary>
        /// <param name="types">Types</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetBaseTypeImplementation(IEnumerable<Type> types)
        {
            return GetBaseTypeImplementation(types, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Get the types implementing the types list
        /// </summary>
        /// <param name="types"></param>
        /// <param name="assemblies">Assemblies where searching types</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetBaseTypeImplementation(IEnumerable<Type> types,
            IEnumerable<Assembly> assemblies)
        {
            var enumerable = types.ToList();
            if (enumerable.Any(x => !x.IsClass)) throw new ArgumentException("Base type must be a class");

            return assemblies
                .SelectMany(x => x.GetTypes())
                .Where(type => type.IsClass
                               && enumerable.Any(type.IsSubclassOf))
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