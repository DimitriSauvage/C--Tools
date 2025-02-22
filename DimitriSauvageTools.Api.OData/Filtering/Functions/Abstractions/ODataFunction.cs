﻿using DimitriSauvageTools.Api.OData.Filtering.Abstractions;

namespace DimitriSauvageTools.Api.OData.Filtering.Functions.Abstractions
{
    public abstract class ODataFunction : IODataFunction, IODataElement
    {
        #region Properties
        /// <summary>
        /// Nom de la fonction
        /// </summary>
        public string Name { get; }
        #endregion

        #region Constructors
        public ODataFunction(string name)
        {
            this.Name = name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère la représentation sous forme d'URL de la fonction
        /// </summary>
        /// <returns></returns>
        public abstract string GetUrlRepresentation();
        #endregion
    }
}
