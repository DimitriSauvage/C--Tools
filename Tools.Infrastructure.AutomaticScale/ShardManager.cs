using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Tools.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tools.Infrastructure.ElasticScale
{
    public class ShardManager
    {
        /// <summary>
        /// Affecte ou obtient le serveur qui héberge les chards
        /// </summary>
        public SqlConnectionStringBuilder ShardManagerDbConnectionStringBuilder { get; private set; }

        public ShardManager(SqlConnectionStringBuilder shardManagerDbConnectionStringBuilder)
        {
            this.ShardManagerDbConnectionStringBuilder = shardManagerDbConnectionStringBuilder;
        }

        /// <summary>
        /// Tries to get the ShardMapManager that is stored in the specified database.
        /// </summary>
        public ShardMapManager TryGetShardMapManager()
        {
            if (!SqlServerDbHelper.DatabaseExists(ShardManagerDbConnectionStringBuilder.ConnectionString, ShardManagerDbConnectionStringBuilder.InitialCatalog))
            {
                // Shard Map Manager database has not yet been created
                return null;
            }

            ShardMapManager shardMapManager;
            bool smmExists = ShardMapManagerFactory.TryGetSqlShardMapManager(
                ShardManagerDbConnectionStringBuilder.ConnectionString,
                ShardMapManagerLoadPolicy.Lazy,
                out shardMapManager);

            if (!smmExists)
            {
                // Shard Map Manager database exists, but Shard Map Manager has not been created
                return null;
            }

            return shardMapManager;
        }

        /// <summary>
        /// Creates a shard map manager in the database specified by the given connection string.
        /// </summary>
        public ShardMapManager CreateOrGetShardMapManager()
        {
            // Get shard map manager database connection string
            // Try to get a reference to the Shard Map Manager in the Shard Map Manager database. If it doesn't already exist, then create it.
            ShardMapManager shardMapManager;
            bool shardMapManagerExists = ShardMapManagerFactory.TryGetSqlShardMapManager(
                ShardManagerDbConnectionStringBuilder.ConnectionString,
                ShardMapManagerLoadPolicy.Lazy,
                out shardMapManager);

            if (shardMapManagerExists)
            {
                //ConsoleUtils.WriteInfo("Shard Map Manager already exists");
            }
            else
            {
                // The Shard Map Manager does not exist, so create it
                shardMapManager = ShardMapManagerFactory.CreateSqlShardMapManager(ShardManagerDbConnectionStringBuilder.ConnectionString);
                //ConsoleUtils.WriteInfo("Created Shard Map Manager");
            }

            return shardMapManager;
        }

        /// <summary>
        /// Creates a new Range Shard Map with the specified name, or gets the Range Shard Map if it already exists.
        /// </summary>
        public RangeShardMap<T> CreateOrGetRangeShardMap<T>(ShardMapManager shardMapManager, string shardMapName)
        {
            // Try to get a reference to the Shard Map.
            RangeShardMap<T> shardMap;
            bool shardMapExists = shardMapManager.TryGetRangeShardMap(shardMapName, out shardMap);

            if (shardMapExists)
            {
                //ConsoleUtils.WriteInfo("Shard Map {0} already exists", shardMap.Name);
            }
            else
            {
                // The Shard Map does not exist, so create it
                shardMap = shardMapManager.CreateRangeShardMap<T>(shardMapName);
                //ConsoleUtils.WriteInfo("Created Shard Map {0}", shardMap.Name);
            }

            return shardMap;
        }

        /// <summary>
        /// Adds Shards to the Shard Map, or returns them if they have already been added.
        /// </summary>
        public Shard CreateOrGetShard(ShardMap shardMap, ShardLocation shardLocation)
        {
            // Try to get a reference to the Shard
            bool shardExists = shardMap.TryGetShard(shardLocation, out Shard shard);

            if (shardExists)
            {
                //ConsoleUtils.WriteInfo("Shard {0} has already been added to the Shard Map", shardLocation.Database);
            }
            else
            {
                // The Shard Map does not exist, so create it
                shard = shardMap.CreateShard(shardLocation);
                //ConsoleUtils.WriteInfo("Added shard {0} to the Shard Map", shardLocation.Database);
            }

            return shard;
        }
    }
}
