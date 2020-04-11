using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Domain.Abstractions;
using Tools.Domain.Helpers;
using Tools.Domain.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Tools.Infrastructure.Plugins
{
    public class ComponentInfo
    {
        #region Properties
        /// <summary>
        /// Affecte ou obtient le nom du composant
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Affecte ou obtient le nom complet du composant
        /// </summary>
        public string FullName { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public ComponentInfo()
        {

        }
        #endregion

        #region Methods

        #endregion


    }
}
