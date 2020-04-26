using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DimitriSauvageTools.Api.OData.Filtering.Abstractions;
using DimitriSauvageTools.Api.OData.Filtering.Enums;

namespace DimitriSauvageTools.Api.OData.Helpers
{
    public static class ODataHelper
    {
        #region Constants

        /// <summary>
        /// Propriété du filtre OData
        /// </summary>
        private const string FilterProperty = "$filter=";

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Récupère les filtres formatés en URL OData
        /// </summary>
        /// <param name="odataFilterQueryParameters">Liste des filtres</param>
        /// <returns></returns>
        public static string GetUrl(IEnumerable<ODataQueryParameter> odataFilterQueryParameters)
        {
            var urlBuilder = new StringBuilder(FilterProperty);

            //Parcours des propriétés afin de récupérer leurs représentation dans les url
            var oDataQueryParameters = odataFilterQueryParameters.ToList();
            for (var i = 0; i < oDataQueryParameters.Count(); i++)
            {
                var filterQueryParameter = oDataQueryParameters.ElementAt(i);
                if (i > 0)
                {
                    urlBuilder.Append(filterQueryParameter.LogicalOperator != null
                        ? $" {Enum.GetName(typeof(ODataLogicalOperators), filterQueryParameter.LogicalOperator)?.ToLower()} "
                        : $" {Enum.GetName(typeof(ODataLogicalOperators), ODataLogicalOperators.And)?.ToLower()} ");
                }

                //Ajout de la représentation
                urlBuilder.Append(filterQueryParameter.GetUrlRepresentation());
            }

            return urlBuilder.ToString();
        }

        #endregion
    }
}