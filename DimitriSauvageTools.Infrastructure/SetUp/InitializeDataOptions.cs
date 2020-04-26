using System;
using Microsoft.AspNetCore.Http;

namespace DimitriSauvageTools.Infrastructure.SetUp
{
    public class InitializeDataOptions
    {
        public string ConfigPath { get; set; } = "/Initialize";

        public Action<HttpContext> Initializer { get; set; }
    }
}
