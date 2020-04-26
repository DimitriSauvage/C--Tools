using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace DimitriSauvageTools.Helpers
{
    public static class ObjectHelper
    {
        #region Properties
        /// <summary>
        /// Récupère les informations sur la méthode du framework réalisant une copie supperficielle
        /// </summary>
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
        #endregion

        #region Methods
        
        #region Public methods
        /// <summary>
        /// Réalise une copie profonde de l'objet
        /// </summary>
        /// <param name="originalObject"></param>
        /// <returns></returns>
        public static Object DeepCopy(this Object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }
        
        /// <summary>
        /// Convertit un dictionnaire de clé valeur en objet dynamique
        /// </summary>
        /// <param name="source">Object source à convertir</param>
        /// <returns>Object dynamique</returns>
        public static dynamic ToDynamicObject(this IDictionary<string, object> source)
        {
            ICollection<KeyValuePair<string, object>> someObject = new ExpandoObject();

            foreach (var item in source)
                someObject.Add(item);
            
            return someObject;
        }
        #endregion
        
        #region Private methods
        /// <summary>
        /// Réalise une copie profonde de l'objet
        /// </summary>
        /// <param name="originalObject">Objet original à copier</param>
        /// <param name="visited">Liste des propriétés et valeurs déjà copiées</param>
        /// <returns>Object copier</returns>
        private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            //Si on recoit null on ne fait pas de copie
            if (originalObject == null) return null;

            //Récupératino du type de l'objet
            var typeToReflect = originalObject.GetType();

            //Si c'est un type primitif on peut directement le renvoyer car il est passé en copie et non en référence
            if (PrimitiveTypesHelper.IsPrimitive(typeToReflect)) return originalObject;

            //Si on a déjà fait la copie on ne va pas la refaire alors on renvoi la copie effectuée
            if (visited.ContainsKey(originalObject)) return visited[originalObject];

            //SI c'est un délégué et qu'on ne peut pas lui assigner notre type on renvoi null
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;

            //Appel de la méthode du framework pour copier les propriétés simples de l'objet
            var cloneObject = CloneMethod.Invoke(originalObject, null);

            //Si mon objet est un tableau
            if (typeToReflect.IsArray)
            {
                //Récupération du type des élements contenus dedans
                var arrayType = typeToReflect.GetElementType();
                if (!PrimitiveTypesHelper.IsPrimitive(arrayType))
                {
                    Array clonedArray = (Array)cloneObject;
                    for (int i = 0; i < clonedArray.Length; i++)
                    {
                        clonedArray.SetValue(InternalCopy(clonedArray.GetValue(i), visited), i);
                    }
                    //Array clonedArray = (Array)cloneObject;
                    //clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            //Ajout de l'objet actuel dans la liste des visités
            visited.Add(originalObject, cloneObject);

            //Copie de tous les champs du type
            CopyFields(originalObject, visited, cloneObject, typeToReflect);

            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }


        /// <summary>
        /// Réalise une copie des champs d'un objet de manière récursive
        /// </summary>
        /// <param name="originalObject">Objet de base à copier</param>
        /// <param name="visited">Liste des objets déjà copiés</param>
        /// <param name="cloneObject">Object cloné</param>
        /// <param name="typeToReflect">Type sur lequel on se trouve</param>
        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            //Traitement si on n'est pas sur le type de base
            if (typeToReflect.BaseType != null)
            {
                //Je copie récursivement tous les champs de mon type de base
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                //Je copie les champs de mon niveau
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        /// <summary>
        /// Réalise la copie des champs pour l'objet
        /// </summary>
        /// <param name="originalObject">Objet dont on copie les champs</param>
        /// <param name="visited">Liste des objets visités</param>
        /// <param name="cloneObject">Objet cloné</param>
        /// <param name="typeToReflect">Type sur lequel on se trouve</param>
        /// <param name="bindingFlags">Informations sur le type du champs</param>
        /// <param name="filter">Filtre à effectuer sur les propriétés</param>
        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            //parcours des propriétés du type
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if ((filter == null || !filter(fieldInfo)) && PrimitiveTypesHelper.IsPrimitive(fieldInfo.FieldType))
                {
                    var originalFieldValue = fieldInfo.GetValue(originalObject);
                    var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                    fieldInfo.SetValue(cloneObject, clonedFieldValue);
                }
            }
        }
        #endregion
                
        #endregion

    }

    /// <summary>
    /// Classe parmettant de réaliser des comparaisons entre objet sur les réferences
    /// </summary>
    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        /// <summary>
        /// Compare deux objets selon leur réference
        /// </summary>
        /// <param name="x">Premier objet à comparer</param>
        /// <param name="y">Deuxième objet à comparer</param>
        /// <returns></returns>
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        /// <summary>
        /// Permet de récupérer le hash d'un objet
        /// </summary>
        /// <param name="obj">Objet dont on souhaite récupérer le hash</param>
        /// <returns></returns>
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

}
