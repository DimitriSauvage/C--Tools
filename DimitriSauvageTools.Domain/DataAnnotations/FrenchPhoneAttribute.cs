﻿using System.ComponentModel.DataAnnotations;
using DimitriSauvageTools.Helpers;

namespace DimitriSauvageTools.Domain.DataAnnotations
{
    /// <summary>
    /// Valide une propriété qui représente un numéro de téléphone au format Français.
    /// </summary>
    public class FrenchPhoneAttribute : ValidationAttribute
    {
        /// <summary>
        /// Détermine si la valeur spécifiée est validée.
        /// Pour cela, elle doit correspondre à un numéro de téléphone au format Français ou ne pas être renseignée.
        /// </summary>
        /// <param name="value">La propriété a valider.</param>
        /// <returns>Vrai si la validation a réussi, faux sinon.</returns>
        public override bool IsValid(object value)
        {
            // Convertie la valeur en string.
            string strValue = value as string;

            // Si elle n'est pas renseignée la propriété est considérée comme valide.
            if (string.IsNullOrEmpty(strValue))
                return true;
            else
                // Sinon on vérifie que la propriété correspond à un numéro de téléphone.
                return PhoneHelper.IsFrenchPhone(strValue);
        }
    }
}
