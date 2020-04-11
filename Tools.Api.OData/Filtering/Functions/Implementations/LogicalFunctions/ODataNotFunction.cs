﻿using Tools.OData.Filtering.Functions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.OData.Filtering.Functions.Implementations.LogicalFunctions
{
    public class ODataNotFunction : ODataLogicalFunction
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
        public ODataNotFunction() : base("not")
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère la représentation sous forme d'URL de la fonction
        /// </summary>
        /// <returns></returns>
        public override string GetUrlRepresentation()
        {
            return $"{this.Name} ";
        }
        #endregion
    }
}
