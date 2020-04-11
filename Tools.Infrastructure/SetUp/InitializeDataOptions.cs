using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.SetUp
{
    public class InitializeDataOptions
    {
        public string ConfigPath { get; set; } = "/Initialize";

        public Action<HttpContext> Initializer { get; set; }
    }
}
