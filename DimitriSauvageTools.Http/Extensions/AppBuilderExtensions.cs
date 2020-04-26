using System;
using Microsoft.AspNetCore.Builder;
using DimitriSauvageTools.Http.Failing;

namespace DimitriSauvageTools.Http.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder)
        {
            return UseFailingMiddleware(builder, null);
        }

        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder, Action<FailingOptions> action)
        {
            var options = new FailingOptions();
            action?.Invoke(options);
            builder.UseMiddleware<FailingMiddleware>(options);
            return builder;
        }
    }
}
