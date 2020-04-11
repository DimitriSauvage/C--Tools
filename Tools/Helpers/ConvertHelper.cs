using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helpers
{
    public static class ConvertHelper
    {
        #region Methods
        public static byte[] ToByteArray(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Convertit une instance d'un type en un objet dynamique
        /// </summary>
        /// <typeparam name="T">Type de l'objet à convertir</typeparam>
        /// <param name="obj">Objet à convertir</param>
        /// <returns>Objet dynamique convertit</returns>
        public static dynamic ToDynamic<T>(this T obj)
        {
            //Création d'un dictionnaire de clé valeur
            IDictionary<string, object> expando = new ExpandoObject();

            //Récupération de toutes les propriétés du type
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                //Ajout d'une clé valeur
                expando.Add(propertyInfo.Name, propertyInfo.GetValue(obj));
            }
            return expando as ExpandoObject;
        }

        /// <summary>
        /// Convertit une liste d'objet en liste de dynamic en conservant les propriétés demandées
        /// </summary>
        /// <typeparam name="T">Type des objets</typeparam>
        /// <param name="objs">Objets à convertir</param>
        /// <param name="propertiesToKeep">Propriétés à conserver</param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics<T>(this IEnumerable<T> objs, IEnumerable<string> propertiesToKeep)
        {
            ICollection<dynamic> results = null;

            if (propertiesToKeep != null && propertiesToKeep.Any())
            {
                results = new List<dynamic>();

                //Propriétés de l'objet
                var objProperties = typeof(T).GetProperties();

                foreach (var obj in objs)
                {
                    IDictionary<string, object> expando = new ExpandoObject();
                    foreach (var propertyTokeep in propertiesToKeep)
                    {
                        object value = ConvertHelper.GetValueForProperty(obj, objProperties, propertyTokeep);
                        expando.Add(propertyTokeep.Split(new char[] { '.' }).Last(), value);
                    }

                    //Ajout de l'objet dans les résultats
                    results.Add(expando as dynamic);
                }
            }
            else
            {
                results = objs.Select(x => x.ToDynamic()).ToList();
            }
            return results;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Récupère la veleur d'une propriété dans un objet
        /// </summary>
        /// <param name="obj">Objet pour lequel récupérer la valeur</param>
        /// <param name="objProperties">Propriétés de l'objet</param>
        /// <param name="property">Propriété pour laquelle récupérer la valeur</param>
        /// <returns></returns>
        private static object GetValueForProperty(object obj, IEnumerable<PropertyInfo> objProperties, string property)
        {
            object result = null;
            //Si la propriété contient des points, alors on veut récupérer la valeur d'une sous propriété
            if (property.Contains("."))
            {
                ICollection<string> subPropertiesNames = property.Split(new char[] { '.' }).ToList();
                string subPropertyName = subPropertiesNames.ElementAt(0);
                subPropertiesNames.Remove(subPropertyName);
                PropertyInfo propertyInfo = null;

                //Recherche de la sous propriété
                propertyInfo = objProperties.FirstOrDefault(x => x.Name.Equals(subPropertyName, StringComparison.InvariantCultureIgnoreCase));

                if (propertyInfo != null)
                {
                    //Si on a trouvé la sous propriété alors on récupère la valeur dans l'objet
                    object propertyValue = propertyInfo.GetValue(obj);

                    if (propertyValue != null)
                    {
                        result = ConvertHelper.GetValueForProperty(propertyValue, propertyValue.GetType().GetProperties(), string.Join(".", subPropertiesNames.ToArray()));
                    }
                }
            }
            else
            {
                //On récupère la propriété
                PropertyInfo propertyInfo = objProperties.FirstOrDefault(x => x.Name.Equals(property, StringComparison.InvariantCultureIgnoreCase));
                if (propertyInfo != null)
                {
                    result = propertyInfo.GetValue(obj);
                }
            }

            return result;
        }
        #endregion

    }
}
