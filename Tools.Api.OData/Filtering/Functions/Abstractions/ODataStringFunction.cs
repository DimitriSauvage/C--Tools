using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Api.OData.Filtering.Functions.Abstractions
{
    public class ODataStringFunction : ODataFunction
    {
        #region Constants

        #endregion

        #region Properties
        /// <summary>
        /// Nom de la propriété de l'objet 
        /// </summary>
        public string PropertyName { get; }
        /// <summary>
        /// Valeur dont la propriété doit commencer par
        /// </summary>
        public string Value { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public ODataStringFunction(string propertyName, string value, string name) : base(name)
        {
            this.PropertyName = !string.IsNullOrWhiteSpace(propertyName) ? propertyName.Replace('.', '/') : null;
            this.Value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère la représentation sous forme d'URL de la fonction
        /// </summary>
        /// <returns>Représentation sous forme d'URL de la fonction</returns>
        public override string GetUrlRepresentation()
        {
            StringBuilder urlBuilder = new StringBuilder(this.Name);
            urlBuilder.Append("(");
            urlBuilder.Append(this.PropertyName);
            urlBuilder.Append(",");
            urlBuilder.Append("'");
            urlBuilder.Append(this.Value);
            urlBuilder.Append("'");
            urlBuilder.Append(")");
            urlBuilder.Append(" eq true");

            return urlBuilder.ToString();
        }
        #endregion
    }
}
