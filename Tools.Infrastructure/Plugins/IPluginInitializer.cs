﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.Plugins
{
    public interface IPluginInitializer
    {
        void ConfigureServices(IServiceCollection serviceCollection);
        void Configure(IApplicationBuilder app, IHostingEnvironment environment);
    }
}
