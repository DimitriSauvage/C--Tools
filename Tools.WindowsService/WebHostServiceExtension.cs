using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Tools.WindowsService
{
    public  static class WebHostServiceExtension
    {
        public static void RunAsCustomService<TService>(this IWebHost host)
        {
            var ctor = typeof(TService).GetConstructor(new[] { typeof(IWebHost) });
            var instance = ctor.Invoke(new[] { host }) as ServiceBase;

            ServiceBase.Run(instance);
        }
    }
}
