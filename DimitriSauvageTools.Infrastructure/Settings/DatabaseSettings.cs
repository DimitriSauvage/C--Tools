using System.Collections.Generic;

namespace DimitriSauvageTools.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        public DatabaseSettings(bool trackEntities)
        {
            TrackEntities = trackEntities;
        }
        public DatabaseSettings() : this(false)
        {

        }

        /// <summary>
        /// Affecte ou obtient le nom de la chaine de connexion à utiliser
        /// </summary>
        public string UsedConnectionString { get; set; }

        /// <summary>
        /// Affecte ou obtient la liste des chaine de connexions
        /// </summary>
        public List<ConnectionStringSettings> ConnectionStrings { get; } = new List<ConnectionStringSettings>();

        /// <summary>
        /// Track the entities or not
        /// </summary>
        public bool TrackEntities { get; set; } = false;
    }

    public class ConnectionStringSettings
    {
        /// <summary>
        /// Affecte ou obtient le nom de la chaine de connexion
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Affecte ou obtient la chaine de connexion à la base de données
        /// </summary>
        public string ConnectionString { get; set; }
    }
}