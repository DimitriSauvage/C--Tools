using Tools.Api.OData.Filtering.Functions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Api.OData.Filtering.Functions.Implementations.ArithmeticsComparison
{
    public class ODataNotEqual : ODataArithmeticComparisonFunction
    {
        #region Constants

        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public ODataNotEqual(string propertyName, long value) : base(propertyName, value, "ne")
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
