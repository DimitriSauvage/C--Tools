using System.Collections.Generic;
using System.Linq;
using System.Text;
using DimitriSauvageTools.Api.OData.Filtering.Abstractions;
using DimitriSauvageTools.Api.OData.Filtering.Enums;

namespace DimitriSauvageTools.Api.OData.Filtering.Models
{
    public class ODataFilterQueryParameter : ODataQueryParameter
    {
        #region Properties
        /// <summary>
        /// Fonctions utilisées pour le filtre
        /// </summary>
        public ICollection<IODataElement> SubQueryElements { get; set; }


        #endregion

        #region Constructors
        public ODataFilterQueryParameter() : this(null)
        {

        }
        public ODataFilterQueryParameter(IODataElement element) : this(element, null)
        {

        }
        public ODataFilterQueryParameter(IODataElement element, ODataLogicalOperators? oDataLogicalOperators) : base(oDataLogicalOperators)
        {
            this.SubQueryElements = new List<IODataElement>();
            this.SubQueryElements.Add(element);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère la representation du paramètre pour une URL OData
        /// </summary>
        /// <returns></returns>
        public override string GetUrlRepresentation()
        {
            StringBuilder urlBuilder = new StringBuilder();

            //Ajout des sous paramètres
            if (this.SubQueryElements != null && this.SubQueryElements.Count() > 0)
            {
                if (this.SubQueryElements.Count > 1) urlBuilder.Append("(");
                foreach (var subQueryElement in this.SubQueryElements)
                {
                    urlBuilder.Append($"{subQueryElement.GetUrlRepresentation()}");
                }
                if (this.SubQueryElements.Count > 1) urlBuilder.Append(")");
            }
            
            return urlBuilder.ToString();
        }
        #endregion
    }


}
