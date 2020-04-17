using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Tools.Exceptions;

namespace Tools.Infrastructure.Helpers
{
    public class SqlServerDbHelper
    {
        /// <summary>
        /// SQL master database name.
        /// </summary>
        public const string MasterDatabaseName = "master";

        /// <summary>
        /// Returns true if we can connect to the database.
        /// </summary>
        public static bool ConnectToSqlDatabase(string masterDbConnectionString)
        {
            try
            {
                using (ReliableSqlConnection conn = new ReliableSqlConnection(
                    masterDbConnectionString,
                    SqlRetryPolicy,
                    SqlRetryPolicy))
                {
                    conn.Open();
                }

                return true;
            }
            catch (SqlException)
            {
                throw new AppException($"Failed to connect to SQL database with connection string : {masterDbConnectionString}");
            }
        }

        /// <summary>
        /// Retourne vrai si la base de données <paramref name="dbName"/> existe
        /// </summary>
        /// <param name="masterDbConnectionString">Chaine de connexion vers la base master</param>
        /// <param name="dbName">Base de donnée recherchée</param>
        /// <returns></returns>
        public static bool DatabaseExists(string masterDbConnectionString, string dbName)
        {
            using (ReliableSqlConnection conn = new ReliableSqlConnection(masterDbConnectionString, SqlRetryPolicy, SqlRetryPolicy))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from sys.databases where name = @dbname";
                cmd.Parameters.AddWithValue("@dbname", dbName);
                cmd.CommandTimeout = 60;
                int count = conn.ExecuteCommand<int>(cmd);

                bool exists = count > 0;
                return exists;
            }
        }

        /// <summary>
        /// Retourne vrai si la base de données souhaitée est en ligne
        /// </summary>
        /// <param name="masterDbConnectionString"></param>
        /// <param name="expectedDbName"></param>
        /// <returns></returns>
        public static bool DatabaseIsOnline(string masterDbConnectionString, string expectedDbName)
        {
            using (ReliableSqlConnection conn = new ReliableSqlConnection(masterDbConnectionString, SqlRetryPolicy, SqlRetryPolicy))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from sys.databases where name = @dbname and state = 0"; // online
                cmd.Parameters.AddWithValue("@dbname", expectedDbName);
                cmd.CommandTimeout = 60;
                int count = conn.ExecuteCommand<int>(cmd);

                bool exists = count > 0;
                return exists;
            }
        }

        /// <summary>
        /// Créé la base de données dont le nom est passé en paramètre <paramref name="dbNameToCreate"/>
        /// </summary>
        /// <param name="masterDbConnectionString">Chaine de connexion vers la base de données master</param>
        /// <param name="dbNameToCreate">Nom de la base de données à créer</param>
        public static void CreateDatabase(string masterDbConnectionString, string dbNameToCreate)
        {
            //ConsoleUtils.WriteInfo("Creating database {0}", db);
            using (ReliableSqlConnection conn = new ReliableSqlConnection(masterDbConnectionString, SqlRetryPolicy, SqlRetryPolicy))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Determine if we are connecting to Azure SQL DB
                cmd.CommandText = "SELECT SERVERPROPERTY('EngineEdition')";
                cmd.CommandTimeout = 60;
                int engineEdition = conn.ExecuteCommand<int>(cmd);

                if (engineEdition == 5)
                {
                    // Azure SQL DB
                    SqlRetryPolicy.ExecuteAction(() =>
                    {
                        if (!DatabaseExists(masterDbConnectionString, dbNameToCreate))
                        {
                            // Begin creation (which is async for Standard/Premium editions)
                            cmd.CommandText = string.Format(
                                "CREATE DATABASE {0} (EDITION = '{1}')",
                                BracketEscapeName(dbNameToCreate),
                                "standard");
                            cmd.CommandTimeout = 60;
                            cmd.ExecuteNonQuery();
                        }
                    });

                    // Wait for the operation to complete
                    while (!DatabaseIsOnline(masterDbConnectionString, dbNameToCreate))
                    {
                        //ConsoleUtils.WriteInfo("Waiting for database {0} to come online...", db);
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                    }

                    //ConsoleUtils.WriteInfo("Database {0} is online", db);
                }
                else
                {
                    // Other edition of SQL DB
                    cmd.CommandText = string.Format("CREATE DATABASE {0}", BracketEscapeName(dbNameToCreate));
                    conn.ExecuteCommand(cmd);
                }
            }
        }

        public static void DropDatabase(string masterDbConnectionString, string dbToDrop)
        {
            using (ReliableSqlConnection conn = new ReliableSqlConnection(
                masterDbConnectionString,
                SqlRetryPolicy,
                SqlRetryPolicy))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Determine if we are connecting to Azure SQL DB
                cmd.CommandText = "SELECT SERVERPROPERTY('EngineEdition')";
                cmd.CommandTimeout = 60;
                int engineEdition = conn.ExecuteCommand<int>(cmd);

                // Drop the database
                if (engineEdition == 5)
                {
                    // Azure SQL DB

                    cmd.CommandText = string.Format("DROP DATABASE {0}", BracketEscapeName(dbToDrop));
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = string.Format(
                        @"ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                        DROP DATABASE {0}",
                        BracketEscapeName(dbToDrop));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ExecuteSqlScript(string masterDbConnectionString, string schemaFile)
        {
            //ConsoleUtils.WriteInfo("Executing script {0}", schemaFile);
            using (ReliableSqlConnection conn = new ReliableSqlConnection(masterDbConnectionString,
                SqlRetryPolicy,
                SqlRetryPolicy))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Read the commands from the sql script file
                IEnumerable<string> commands = ReadSqlScript(schemaFile);

                foreach (string command in commands)
                {
                    cmd.CommandText = command;
                    cmd.CommandTimeout = 60;
                    conn.ExecuteCommand(cmd);
                }
            }
        }

        private static IEnumerable<string> ReadSqlScript(string scriptFile)
        {
            List<string> commands = new List<string>();
            using (TextReader tr = new StreamReader(scriptFile))
            {
                StringBuilder sb = new StringBuilder();
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line == "GO")
                    {
                        commands.Add(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }
                }
            }

            return commands;
        }

        /// <summary>
        /// Escapes a SQL object name with brackets to prevent SQL injection.
        /// </summary>
        private static string BracketEscapeName(string sqlName)
        {
            return '[' + sqlName.Replace("]", "]]") + ']';
        }

        /// <summary>
        /// Gets the retry policy to use for connections to SQL Server.
        /// </summary>
        public static RetryPolicy SqlRetryPolicy
        {
            get
            {
                return new RetryPolicy<ExtendedSqlDatabaseTransientErrorDetectionStrategy>(10, TimeSpan.FromSeconds(5));
            }
        }

        /// <summary>
        /// Extended sql transient error detection strategy that performs additional transient error
        /// checks besides the ones done by the enterprise library.
        /// </summary>
        private class ExtendedSqlDatabaseTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
        {
            /// <summary>
            /// Enterprise transient error detection strategy.
            /// </summary>
            private SqlDatabaseTransientErrorDetectionStrategy _sqltransientErrorDetectionStrategy = new SqlDatabaseTransientErrorDetectionStrategy();

            /// <summary>
            /// Checks with enterprise library's default handler to see if the error is transient, additionally checks
            /// for such errors using the code in the in <see cref="IsTransientException"/> function.
            /// </summary>
            /// <param name="ex">Exception being checked.</param>
            /// <returns><c>true</c> if exception is considered transient, <c>false</c> otherwise.</returns>
            public bool IsTransient(Exception ex)
            {
                return _sqltransientErrorDetectionStrategy.IsTransient(ex);
            }
        }
    }
}
