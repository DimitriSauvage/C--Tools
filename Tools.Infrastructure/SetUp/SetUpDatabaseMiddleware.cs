using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tools.Http.Extensions;
using Tools.Infrastructure.Abstraction;
using Tools.Infrastructure.Models;
using Tools.Infrastructure.Settings;

namespace Tools.Infrastructure.SetUp
{
    public class SetUpDatabaseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly SetUpDatabaseOptions options;
        private readonly IWritableOptions<DatabaseSettings> appSettings;

        public SetUpDatabaseMiddleware(RequestDelegate next, SetUpDatabaseOptions options, IWritableOptions<DatabaseSettings> appSettings)
        {
            this.next = next;
            this.options = options;
            this.appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.Equals(options.ConfigPath, StringComparison.OrdinalIgnoreCase))
            {
                await ProcessConfigRequest(context);
                return;
            }

            await next.Invoke(context);
        }

        private async Task ProcessConfigRequest(HttpContext context)
        {
            var setupSettings = context.ReadContentAsAsync<SetUpDTO>().Result;

            switch (setupSettings.ServerMode)
            {
                case SetUpDTO.ServerModeEnum.Cloud:
                    #region Paramétrage de la base de données en mode cloud
                    throw new NotImplementedException("Not implemented yet");
                    #endregion
                    //break;
                case SetUpDTO.ServerModeEnum.Server:
                    #region Paramétrage de la base de données en mode serveur                   
                    if (setupSettings.DatabaseSettings == null)
                        throw new ArgumentException($"L'objet de type {nameof(DatabaseSettingsDTO)} vaut null.");

                    // Mise à jour des paramètres dans le fichier de configuration
                    string connectionString = setupSettings.DatabaseSettings.ToConnectionString();
                    appSettings.Update(settings =>
                    {
                        string defaultConnectionStringName = "Default";

                        if (settings.ConnectionStrings.Any(connection => connection.Name == defaultConnectionStringName))
                            settings.ConnectionStrings.FirstOrDefault(connection => connection.Name == defaultConnectionStringName).ConnectionString = setupSettings.DatabaseSettings.ToConnectionString();
                        else
                            settings.ConnectionStrings.Add(new ConnectionStringSettings { Name = defaultConnectionStringName, ConnectionString = connectionString });

                        settings.UsedConnectionString = defaultConnectionStringName;
                    });

                    // Application des migrations
                    var dbContextFactoryType = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(t => typeof(IDesignTimeDbContextFactory<DbContext>).IsAssignableFrom(t));

                    if (dbContextFactoryType != null)
                    {
                        var dbContextFactory = Activator.CreateInstance(dbContextFactoryType) as IDesignTimeDbContextFactory<DbContext>;
                        dbContextFactory.CreateDbContext(new string[] { });
                    }
                    #endregion
                    break;
            }
            
            // If reach here, that means that no valid parameter has been passed. Just output status
            await SendOkResponse(context, string.Format("La chaine de connexion a été affectée"));
            return;
        }

        private async Task SendOkResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(message);
        }
    }
}
