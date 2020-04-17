using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Tools.Infrastructure.Extensions;

namespace Tools.Infrastructure.SetUp
{
    public class SetUpDatabaseStartupFilter : IStartupFilter
    {
        private readonly Action<SetUpDatabaseOptions> options;

        public SetUpDatabaseStartupFilter(Action<SetUpDatabaseOptions> optionsAction)
        {
            options = optionsAction;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseSetupDatabaseMiddleware(options);
                next(app);
            };
        }
    }
}
