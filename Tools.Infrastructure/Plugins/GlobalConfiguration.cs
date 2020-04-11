using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.Plugins
{
    public static class GlobalConfiguration
    {
        #region Properties
        public static IList<PluginInfo> Plugins { get; set; }
        /// <summary>
        /// Affecte ou obtient le chemin de la racine du dossier web
        /// </summary>
        public static string WebRootPath { get; set; }
        /// <summary>
        /// Affecte ou obtient le chemin de la racine du contenu
        /// </summary>
        public static string ContentRootPath { get; set; }
        #endregion
        
        static GlobalConfiguration()
        {
            Plugins = new List<PluginInfo>();
        }
    }
}
