using System;
using System.ComponentModel.DataAnnotations;

namespace DimitriSauvageTools.Domain.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Affecte ou obtient la valeur minimale
        /// </summary>
        public double MinValue { get; set; }

        #endregion

        #region Constructors

        public MinAttribute(double min)
        {
            this.MinValue = min;
        }

        #endregion

        #region Methods
        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType() != typeof(double))
                return new ValidationResult("La valeur doit-être de type double");

            if (((double) value) < MinValue)
                return new ValidationResult(
                    $"La valeur de {validationContext.DisplayName} doit-être supérieure à {MinValue}");

            return null;
        }

        #endregion
    }
}