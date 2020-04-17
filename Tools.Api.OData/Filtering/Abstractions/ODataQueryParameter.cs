using Tools.Api.OData.Filtering.Enums;

namespace Tools.Api.OData.Filtering.Abstractions
{
    public abstract class ODataQueryParameter : IODataElement, IODataQueryParameter
    {
        #region Properties
        /// <summary>
        /// Opérateur logique à utiliser pour séparer ce paramètre du précédent
        /// </summary>
        public ODataLogicalOperators? LogicalOperator { get; set; }
        #endregion

        #region Constructors
        public ODataQueryParameter(ODataLogicalOperators? logicalOperator)
        {
            this.LogicalOperator = logicalOperator;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Récupère la representation du paramètre pour une URL OData
        /// </summary>
        /// <returns></returns>
        public abstract string GetUrlRepresentation();
        #endregion

    }
}
