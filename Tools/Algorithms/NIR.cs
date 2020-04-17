using System;
using Tools.Helpers;
using Tools.Languages;

namespace Tools.Algorithms
{
    /// <summary>
    /// Outil de vérification des NIR (Numéro d'Inscription au Répertoire des personnes physiques)
    /// usuellement dénommé "numéro de sécurité sociale" en France.
    /// http://fr.wikipedia.org/wiki/Num%C3%A9ro_de_s%C3%A9curit%C3%A9_sociale_en_France
    /// </summary>
    /// <remarks>
    /// Contribution de DelphiManiac
    /// http://www.developpez.net/forums/u216/delphimaniac/
    /// </remarks>
    public static class NIR
    {
        /// <summary>
        /// Taille exacte du NIR.
        /// </summary>
        private const int _NIRExactLength = 13;
        /// <summary>
        /// Taille exacte d'une clé de NIR.
        /// </summary>
        private const int _NIRKeyExactLength = 2;

        /// <summary>
        /// Calcule la clé d'un NIR.
        /// </summary>
        /// <param name="nir">NIR dont on désire calculer la clé.</param>
        /// <returns>La clé calculée.</returns>
        /// <exception cref="ArgumentException">Le NIR est invalide.</exception>
        /// <exception cref="ArgumentNullException">Le NIR est null.</exception>
        public static string CalcKey(string nir)
        {
            if (string.IsNullOrEmpty(nir))
                throw new ArgumentNullException(ExceptionMessage.StringNullOrEmpty);
            if (nir.Length != _NIRExactLength)
                throw new ArgumentException(string.Format(ExceptionMessage.StringWrongLength, _NIRExactLength));

            string dep = nir.Substring(5, 2);
            string newDep = "";
            if (dep == "2A")
            {
                newDep = "19";
            }
            else if (dep == "2B")
            {
                newDep = "18";
            }
            if (newDep != "")
            {
                nir = nir.Left(5) + newDep + nir.Right(6);
            }

            decimal nirAsDecimal;
            if (!decimal.TryParse(nir, out nirAsDecimal))
            {
                throw new ArgumentException(ExceptionMessage.InvalidNir);
            }
            return (97 - (nirAsDecimal % 97)).ToString("00");
        }

        /// <summary>
        /// Vérifie la validité d'un couple NIR / Clé.
        /// </summary>
        /// <param name="nir">NIR à contrôler (13 chiffres, sauf  numéro de département qui peut être 2A ou 2B).</param>
        /// <param name="nirKey">Clé à contrôler (2 chiffres).</param>
        /// <returns>Vrai si la clé est valide pour ce NIR, faux sinon.</returns>
        /// <exception cref="ArgumentException">Le NIR et/ou la clé sont invalides.</exception>
        /// <exception cref="ArgumentNullException">Le NIR et/ou la clé sont null.</exception>
        public static bool Check(string nir, string nirKey)
        {
            int unusedInt;
            if (string.IsNullOrEmpty(nir) || string.IsNullOrEmpty(nirKey))
                throw new ArgumentNullException(ExceptionMessage.StringNullOrEmpty);
            if (nirKey.Length != _NIRKeyExactLength || !int.TryParse(nirKey, out unusedInt))
                throw new ArgumentException(string.Format(ExceptionMessage.StringWrongLength, _NIRKeyExactLength));
            return CalcKey(nir) == nirKey;
        }
    }
}
