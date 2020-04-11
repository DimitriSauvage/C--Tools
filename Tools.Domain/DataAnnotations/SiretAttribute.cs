using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Domain.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SiretAttribute : ValidationAttribute
    {
        #region Methods
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            if (Tools.Algorithms.Siret.Check(value.ToString()))
                return null;
            else
                return new ValidationResult("Le numéro de SIRET n'est pas valide.");
        }
        #endregion
    }
}
