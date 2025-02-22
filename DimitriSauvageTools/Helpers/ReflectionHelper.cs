﻿using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace DimitriSauvageTools.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Obtient dans un dictionnaire clé / valeur l'ensemble des propriétés et des valeurs de l'objet passé en paramètre
        /// </summary>
        /// <param name="obj">Objet à inspecter</param>
        /// <returns></returns>
        public static String PrintProperties(object obj, int indent)
        {
            if (obj == null) return null;

            StringBuilder ret = new StringBuilder();

            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);
                var elems = propValue as IList;
                if (elems != null)
                {
                    foreach (var item in elems)
                    {
                        ret.AppendLine(string.Format("{0}{1}[{2}]:", indentString, property.Name, elems.IndexOf(item)));
                        ret.AppendLine(PrintProperties(item, indent + 3));
                    }
                }
                else
                {
                    // This will not cut-off System.Collections because of the first check
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        ret.AppendLine(string.Format("{0}{1}: {2}", indentString, property.Name, propValue != null ? string.Empty : "<null>"));
                        if (propValue != null)
                            ret.AppendLine(PrintProperties(propValue, indent + 2));
                    }
                    else
                    {
                        ret.AppendLine(string.Format("{0}{1}: {2}", indentString, property.Name, propValue != null ? propValue : "<null>"));
                    }
                }
            }

            return ret.ToString();
        }

        /// <summary>
        /// Tente de définir une valeur pour une propriété
        /// </summary>
        /// <param name="propertyInfo">Propriété sur laquelle définir la valeur</param>
        /// <param name="objectToDefineValue">Objet sur lequel on affecte la valeur</param>
        /// <param name="value">Valeur à affecter</param>
        /// <returns></returns>
        public static bool TrySetValue(this PropertyInfo propertyInfo, object objectToDefineValue, object value)
        {
            bool result = false;
            try
            {
                propertyInfo.SetValue(objectToDefineValue, value);
                result = true;
                
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }
    }
}
