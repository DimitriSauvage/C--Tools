using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Domain.Abstractions;
using Tools.Domain.Helpers;
using Tools.Domain.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Infrastructure.Plugins
{
    public class PluginInfo
    {
        #region Properties
        /// <summary>
        /// Affecte ou obtient le nom complet du plugin
        /// </summary>
        [UniqueKey]
        public string Name { get; set; }
        /// <summary>
        /// Affecte ou obtient l'assembly
        /// </summary>
        public Assembly Assembly { get; set; }
        /// <summary>
        /// Affecte ou obtient le nom court du plugin
        /// </summary>
        public string ShortName { get { return Name?.Split('.')?.Last(); } }

        /// <summary>
        /// Affecte ou obtient le chemin vers le plugin
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Affecte ou obtient les pages présentes dans le plugin
        /// </summary>
        public ICollection<ViewInfo> Views { get; set; } = new HashSet<ViewInfo>();
        /// <summary>
        /// Affecte ou obtient les composants décrits dans le plugin
        /// </summary>
        //public ICollection<ComponentInfo> Components { get; set; } = new HashSet<ComponentInfo>();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public PluginInfo()
        {

        }
        #endregion

        #region Methods

        #endregion

    }
}
