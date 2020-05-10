using System.Collections.Generic;
using DimitriSauvageTools.Infrastructure.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DimitriSauvageTools.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        #region Fields

        /// <summary>
        /// Get or set the name of the used connection string
        /// </summary>
        public string UsedConnectionString { get; set; }

        /// <summary>
        /// Get or set the connection string list
        /// </summary>
        public ICollection<ConnectionStringSettings> ConnectionStrings { get; } = new List<ConnectionStringSettings>();

        /// <summary>
        /// Track the entities or not
        /// </summary>
        public bool TrackEntities { get; set; } = false;

        /// <summary>
        /// Get or set the type of the used database
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DbType DbType { get; set; }

        #endregion


        #region Constructors

        public DatabaseSettings(bool trackEntities)
        {
            TrackEntities = trackEntities;
        }

        public DatabaseSettings() : this(false)
        {
        }

        #endregion
    }
}