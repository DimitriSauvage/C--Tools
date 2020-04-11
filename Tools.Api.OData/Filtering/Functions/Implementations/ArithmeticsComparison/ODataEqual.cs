using Tools.OData.Filtering.Functions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.OData.Filtering.Functions.Implementations.ArithmeticsComparison
{
    public class ODataEqual : ODataArithmeticComparisonFunction
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
        public ODataEqual(string propertyName, long value) : base(propertyName, value, "eq")
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
