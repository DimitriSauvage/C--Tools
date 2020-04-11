using Tools.OData.Filtering.Enums;
using Tools.OData.Filtering.Functions.Abstractions;
using Tools.OData.Filtering.Functions.Implementations.ArithmeticsComparison;
using Tools.OData.Filtering.Functions.Implementations.LogicalFunctions;
using Tools.OData.Filtering.Functions.Implementations.StringFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.OData.Filtering.Functions.Factories
{
    public class ODataFunctionFactory
    {
        /// <summary>
        /// Récupèrer la fonction adéquate selon l'opérateur et le type de champ
        /// </summary>
        /// <param name="oDataLogicalOperators"></param>
        /// <param name="oDataBaseFieldType"></param>
        /// <returns></returns>
        public ODataFunction GetFunction(ODataLogicalOperators oDataLogicalOperators, string propertyName, string value = null)
        {
            ODataFunction oDataFunction = null;

            switch (oDataLogicalOperators)
            {
                case ODataLogicalOperators.Eq:
                    oDataFunction = new ODataEqual(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.Ne:
                    oDataFunction = new ODataNotEqual(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.Gt:
                    oDataFunction = new ODataGreaterThan(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.Ge:
                    oDataFunction = new ODataGreaterThanOrEqual(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.Lt:
                    oDataFunction = new ODataLessThan(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.Le:
                    oDataFunction = new ODataLessThanOrEqual(propertyName, this.GetParsedValue(value));
                    break;
                case ODataLogicalOperators.StartsWith:
                    oDataFunction = new ODataStartsWith(propertyName, value);
                    break;
                case ODataLogicalOperators.And:
                    oDataFunction = new ODataAndFunction();
                    break;
                case ODataLogicalOperators.Or:
                    oDataFunction = new ODataOrFunction();
                    break;
                case ODataLogicalOperators.Not:
                    oDataFunction = new ODataNotFunction();
                    break;
                default:
                    break;
            }

            return oDataFunction;
        }

        #region Private methods
        /// <summary>
        /// Essaye de transformer une chaine en numérique
        /// </summary>
        /// <param name="value">Valeur qui sera affectée</param>
        /// <param name="valueToParse">Valeur à tranformer</param>
        /// <returns></returns>
        private bool TryParseValue(out long value, string valueToParse)
        {
            return long.TryParse(valueToParse, out value);
        }

        /// <summary>
        /// Récupère la valeur après la transformation
        /// </summary>
        /// <param name="valueToParse"></param>
        /// <returns>Valeur par défaut du type si la conversion n'a pas réussi</returns>
        private long GetParsedValue(string valueToParse)
        {
            long value = default(long);
            if (!string.IsNullOrWhiteSpace(valueToParse)) this.TryParseValue(out value, valueToParse.Trim());
            return value;
        }
        #endregion
    }
}
