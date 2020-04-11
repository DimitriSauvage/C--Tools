using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Domain.DataAnnotations
{
    /// <summary>
    /// Attribute qui placé sur une propriété d'un objet modèle, indique que celle-ci sera mappée comme composant la clé unique de l'objet
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UniqueByAttribute : Attribute
    {
        /// <summary>
        /// Liste des propriétés composants la clé unique de l'objet
        /// </summary>
        public IEnumerable<string> PropertyNames { get; private set; }
        
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="hashCodeProps">Liste des propriétés</param>
        public UniqueByAttribute(params string[] hashCodeProps)
        {
            this.PropertyNames = hashCodeProps;
        }
    }
}
