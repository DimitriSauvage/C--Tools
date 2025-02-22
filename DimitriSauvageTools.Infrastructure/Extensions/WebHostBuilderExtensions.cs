﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DimitriSauvageTools.Infrastructure.SetUp;

namespace DimitriSauvageTools.Infrastructure.Extensions
{
    public static class WebHostBuildertExtensions
    {
        public static IWebHostBuilder UseSetupDatabaseByEndpoint(this IWebHostBuilder builder)
        {
            return UseSetupDatabaseByEndpoint(builder, options => { options.ConfigPath = "/setup/database"; });
        }

        public static IWebHostBuilder UseSetupDatabaseByEndpoint(this IWebHostBuilder builder, Action<SetUpDatabaseOptions> options)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new SetUpDatabaseStartupFilter(options));
            });
            return builder;
        }

        public static IWebHostBuilder UseSetupDataByEndpoint(this IWebHostBuilder builder)
        {
            return UseSetupDataByEndpoint(builder, options => { options.ConfigPath = "/initialize"; });
        }

        public static IWebHostBuilder UseSetupDataByEndpoint(this IWebHostBuilder builder, Action<InitializeDataOptions> options)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new InitializeDataStartupFilter(options));
            });
            return builder;
        }
    }
}
