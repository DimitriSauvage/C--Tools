using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
    }
}
