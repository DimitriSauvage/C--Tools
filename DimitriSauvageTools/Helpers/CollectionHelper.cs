using System.Collections.Generic;

namespace DimitriSauvageTools.Helpers
{
    public static class CollectionHelper
    {
        /// <summary>
        /// Ajoute un ensemble d'élement dans une collection
        /// </summary>
        /// <typeparam name="T">Type des élement à ajouter</typeparam>
        /// <param name="collection">Collection dans laquelle ajouter les élements</param>
        /// <param name="items">Éléments à ajouter</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
