using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Api.OData.Filtering.Functions.Abstractions
{
    public abstract class ODataArithmeticComparisonFunction : ODataFunction
    {
        #region Constants

        #endregion

        #region Properties
        /// <summary>
        /// Nom de la propriété de l'objet 
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Valeur dont la propriété doit commencer par
        /// </summary>
        public long Value { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public ODataArithmeticComparisonFunction(string propertyName, long value, string name) : base(name)
        {
            this.PropertyName = propertyName.Replace('.', '/');
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
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(this.PropertyName);
            urlBuilder.Append($" {this.Name} ");
            urlBuilder.Append(this.Value);

            return urlBuilder.ToString();
        }
        #endregion
    }
}