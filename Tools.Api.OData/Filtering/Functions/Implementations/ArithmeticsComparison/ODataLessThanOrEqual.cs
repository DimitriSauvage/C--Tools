using Tools.Api.OData.Filtering.Functions.Abstractions;

namespace Tools.Api.OData.Filtering.Functions.Implementations.ArithmeticsComparison
{
    public class ODataLessThanOrEqual : ODataArithmeticComparisonFunction
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
        public ODataLessThanOrEqual(string propertyName, long value) : base(propertyName, value, "le")
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
