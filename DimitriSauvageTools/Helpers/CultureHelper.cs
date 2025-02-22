﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DimitriSauvageTools.Helpers
{
    public static class CultureHelper
    {
        #region Attributes
        /// <summary>
        /// Liste des cultures
        /// </summary>
        private static readonly IList<string> allCultures = new List<string> { "af", "af-ZA", "sq", "sq-AL", "gsw-FR", "am-ET", "ar", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "hy", "hy-AM", "as-IN", "az", "az-Cyrl-AZ", "az-Latn-AZ", "ba-RU", "eu", "eu-ES", "be", "be-BY", "bn-BD", "bn-IN", "bs-Cyrl-BA", "bs-Latn-BA", "br-FR", "bg", "bg-BG", "ca", "ca-ES", "zh-HK", "zh-MO", "zh-CN", "zh-Hans", "zh-SG", "zh-TW", "zh-Hant", "co-FR", "hr", "hr-HR", "hr-BA", "cs", "cs-CZ", "da", "da-DK", "prs-AF", "div", "div-MV", "nl", "nl-BE", "nl-NL", "en", "en-AU", "en-BZ", "en-CA", "en-029", "en-IN", "en-IE", "en-JM", "en-MY", "en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT", "en-GB", "en-US", "en-ZW", "et", "et-EE", "fo", "fo-FO", "fil-PH", "fi", "fi-FI", "fr", "fr-BE", "fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "fy-NL", "gl", "gl-ES", "ka", "ka-GE", "de", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el", "el-GR", "kl-GL", "gu", "gu-IN", "ha-Latn-NG", "he", "he-IL", "hi", "hi-IN", "hu", "hu-HU", "is", "is-IS", "ig-NG", "id", "id-ID", "iu-Latn-CA", "iu-Cans-CA", "ga-IE", "xh-ZA", "zu-ZA", "it", "it-IT", "it-CH", "ja", "ja-JP", "kn", "kn-IN", "kk", "kk-KZ", "km-KH", "qut-GT", "rw-RW", "sw", "sw-KE", "kok", "kok-IN", "ko", "ko-KR", "ky", "ky-KG", "lo-LA", "lv", "lv-LV", "lt", "lt-LT", "wee-DE", "lb-LU", "mk", "mk-MK", "ms", "ms-BN", "ms-MY", "ml-IN", "mt-MT", "mi-NZ", "arn-CL", "mr", "mr-IN", "moh-CA", "mn", "mn-MN", "mn-Mong-CN", "ne-NP", "no", "nb-NO", "nn-NO", "oc-FR", "or-IN", "ps-AF", "fa", "fa-IR", "pl", "pl-PL", "pt", "pt-BR", "pt-PT", "pa", "pa-IN", "quz-BO", "quz-EC", "quz-PE", "ro", "ro-RO", "rm-CH", "ru", "ru-RU", "smn-FI", "smj-NO", "smj-SE", "se-FI", "se-NO", "se-SE", "sms-FI", "sma-NO", "sma-SE", "sa", "sa-IN", "sr", "sr-Cyrl-BA", "sr-Cyrl-SP", "sr-Latn-BA", "sr-Latn-SP", "nso-ZA", "tn-ZA", "si-LK", "sk", "sk-SK", "sl", "sl-SI", "es", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-US", "es-UY", "es-VE", "sv", "sv-FI", "sv-SE", "syr", "syr-SY", "tg-Cyrl-TJ", "tzm-Latn-DZ", "ta", "ta-IN", "tt", "tt-RU", "te", "te-IN", "th", "th-TH", "bo-CN", "tr", "tr-TR", "tk-TM", "ug-CN", "uk", "uk-UA", "wen-DE", "ur", "ur-PK", "uz", "uz-Cyrl-UZ", "uz-Latn-UZ", "vi", "vi-VN", "cy-GB", "wo-SN", "sah-RU", "ii-CN", "yo-NG" };

        /// <summary>
        /// Liste des cultures qui sont implémentées
        /// </summary>
        private static readonly IList<string> availableCultures = new List<string> { "fr", "en" };
        #endregion

        #region Properties
        /// <summary>
        /// Obtient un booléen qui indique si le texte doit-être écrit de la droite vers la gauche
        /// </summary>
        public static bool IsRigthToLeft { get { return Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft; } }

        /// <summary>
        /// Obtient la culture par défaut "fr-FR"
        /// </summary>
        public static string DefaultCulture { get { return availableCultures.First(); } }

        /// <summary>
        /// Obtient la culture associée au thread en cours
        /// </summary>
        /// <returns></returns>
        public static string CurrentCultureName
        {
            get { return Thread.CurrentThread.CurrentCulture.Name; }
        }

        /// <summary>
        /// Obtient la culture neutre associée au thread en cours
        /// </summary>
        public static string GetCurrentNeutralCultureName
        {
            get { return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtient la culture qui est implémentée
        /// </summary>
        /// <param name="cultureName">Nom de la culture (ie. fr-FR)</param>
        /// <returns>Le nom de la culture à utiliser</returns>
        public static string GetImplementedCultureName(string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                return DefaultCulture; // return la culture par défaut

            // Vérification que la culture passée en paramètre est valide
            if (!allCultures.Any(c => c.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase)))
                return DefaultCulture; // Si invalide, retourne la culture par défaut

            // Vérification que la culture passée en paramètre fait bien partie de la liste des cultures valides
            if (availableCultures.Any(c => c.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase)))
                return cultureName;

            // Dernière vérification, si la culture passée en paramètre est 'en-GB' mais que la culture implémentée est 'en-US'
            // On retourne alors la culture neutre car la langue de base est la même
            var neutralCultureName = GetNeutralCulture(cultureName);
            foreach (var c in availableCultures)
                if (c.StartsWith(neutralCultureName))
                    return c;

            return DefaultCulture;
        }

        /// <summary>
        /// Obtient la culture neutre
        /// </summary>
        /// <example>si la culture passée en paramètre est fr-FR, retournera alors fr</example>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public static string GetNeutralCulture(string cultureName)
        {
            if (!cultureName.Contains("-"))
                return cultureName;

            return cultureName.Split('-')[0];
        }
        #endregion
    }
}
