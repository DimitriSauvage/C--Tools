using DimitriSauvageTools.Api.OData.Filtering.Functions.Abstractions;

namespace DimitriSauvageTools.Api.OData.Filtering.Functions.Implementations.ArithmeticsComparison
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
