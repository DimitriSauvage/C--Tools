using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DimitriSauvageTools.Api.OData.Filtering.Abstractions;
using DimitriSauvageTools.Api.OData.Filtering.Enums;

namespace DimitriSauvageTools.Helpers
{
    public static class ODataHelper
    {
        #region Constants
        /// <summary>
        /// Propriété du filtre OData
        /// </summary>
        private const string filterProperty = "$filter=";
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
            StringBuilder urlBuilder = new StringBuilder(filterProperty);

            //Parcours des propriétés afin de récupérer leurs représentation dans les url
            for (int i = 0; i < odataFilterQueryParameters.Count(); i++)
            {
                ODataQueryParameter filterQueryParameter = odataFilterQueryParameters.ElementAt(i);
                if (i > 0)
                {
                    if (filterQueryParameter.LogicalOperator != null)
                    {
                        urlBuilder.Append($" {Enum.GetName(typeof(ODataLogicalOperators), filterQueryParameter.LogicalOperator).ToLower()} ");
                    }
                    else
                    {
                        urlBuilder.Append($" {Enum.GetName(typeof(ODataLogicalOperators), ODataLogicalOperators.And).ToLower()} ");
                    }
                }

                //Ajout de la représentation
                urlBuilder.Append(filterQueryParameter.GetUrlRepresentation());
            }

            return urlBuilder.ToString();
        }
        #endregion
    }
}
