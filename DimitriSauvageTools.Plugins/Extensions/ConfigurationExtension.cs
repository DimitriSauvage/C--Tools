using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using DimitriSauvageTools.Plugins.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DimitriSauvageTools.Plugins.Extensions
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Charge les composants indiqués dans le fichier appsettings.json
        /// </summary>
        /// <param name="services">Collection de services</param>
        /// <param name="config">Configuration</param>
        public static void AddPlugins(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                var components = config.GetSection("Components").Get<Config.Components>();
                foreach (var item in components.Assemblies)
                    options.FileProviders.Add(new EmbeddedFileProvider(Assembly.Load(item.Assembly)));
            });
        }
    }
}
