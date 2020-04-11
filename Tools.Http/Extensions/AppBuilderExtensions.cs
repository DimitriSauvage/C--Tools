using Microsoft.AspNetCore.Builder;
using Tools.Http.Failing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Http.Extensions
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
