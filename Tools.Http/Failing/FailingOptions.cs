using System.Collections.Generic;

namespace Tools.Http.Failing
{
    public class FailingOptions
    {
        public string ConfigPath { get; set; } = "/Failing";
        public List<string> EndpointPaths { get; set; } = new List<string>();
    }
}