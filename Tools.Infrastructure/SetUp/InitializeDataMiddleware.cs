using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tools.Http.Extensions;
using Tools.Infrastructure.Models;
using Tools.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Tools.Infrastructure.Abstraction;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tools.Infrastructure.SetUp
{
    /// <summary>
    /// Middleware chargé de créer un endpoint d'initialisation des données pour une API
    /// </summary>
    public class InitializeDataMiddleware
    {
        private readonly RequestDelegate next;
        private readonly InitializeDataOptions options;
        private readonly IWritableOptions<AppSettings> appSettings;

        public InitializeDataMiddleware(RequestDelegate next, InitializeDataOptions options, IWritableOptions<AppSettings> appSettings)
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
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(message);
        }
    }
}
