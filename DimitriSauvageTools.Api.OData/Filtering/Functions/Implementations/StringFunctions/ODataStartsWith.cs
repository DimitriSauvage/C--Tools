﻿using DimitriSauvageTools.Api.OData.Filtering.Functions.Abstractions;

namespace DimitriSauvageTools.Api.OData.Filtering.Functions.Implementations.StringFunctions
{
    public class ODataStartsWith : ODataStringFunction
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
        public ODataStartsWith(string propertyName, string value) : base(propertyName, value, "startswith")
        {
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
