using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Domain.DataAnnotations
{
    /// <summary>
    /// Attribute qui placé sur une propriété d'un objet modèle, indique que celle-ci sera mappée comme composant la clé unique de l'objet
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UniqueKeyAttribute : Attribute
    {
    }
}
