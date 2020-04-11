using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Api.Abstractions
{
    public abstract class BaseController : Controller
    {
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Newtonsoft.Json.Formatting.Indented,
            MaxDepth = 1
        };
    }
}
