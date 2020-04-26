using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DimitriSauvageTools.Infrastructure.Abstraction;
using DimitriSauvageTools.Infrastructure.Settings;

namespace DimitriSauvageTools.Infrastructure.SetUp
{
    /// <summary>
    /// Middleware chargé de créer un endpoint d'initialisation des données pour une API
    /// </summary>
    public class InitializeDataMiddleware
    {
        private readonly RequestDelegate next;
        private readonly InitializeDataOptions options;
        private readonly IWritableOptions<DatabaseSettings> appSettings;

        public InitializeDataMiddleware(RequestDelegate next, InitializeDataOptions options, IWritableOptions<DatabaseSettings> appSettings)
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
            options.Initializer?.Invoke(context);

            // If reach here, that means that no valid parameter has been passed. Just output status
            await SendOkResponse(context, string.Format("Les données ont été initialisées"));
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
