using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tools.Http.Failing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Http.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseFailing(this IWebHostBuilder builder, Action<FailingOptions> options)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new FailingStartupFilter(options));
            });
            return builder;
        }
    }
}
