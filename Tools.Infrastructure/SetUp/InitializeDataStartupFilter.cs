using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Tools.Infrastructure.Extensions;

namespace Tools.Infrastructure.SetUp
{
    public class InitializeDataStartupFilter : IStartupFilter
    {
        private readonly Action<InitializeDataOptions> options;

        public InitializeDataStartupFilter(Action<InitializeDataOptions> optionsAction)
        {
            options = optionsAction;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseInitializeDataMiddleware(options);
                
                next(app);
            };
        }
    }
}
