using Tools.OData.Filtering.Functions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.OData.Filtering.Functions.Implementations.ArithmeticsComparison
{
    public class ODataLessThan : ODataArithmeticComparisonFunction
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
        public ODataLessThan(string propertyName, long value) : base(propertyName, value, "lt")
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
