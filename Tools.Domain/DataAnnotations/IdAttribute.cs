using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Domain.DataAnnotations
{
    /// <summary>
    /// Attribute qui placé sur une propriété d'un objet modèle, indique que celle-ci sera mappée comme identifiant
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IdAttribute : Attribute
    {
    }
}
