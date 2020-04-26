using System.Collections.Generic;

namespace DimitriSauvageTools.Http.Failing
{
    public class FailingOptions
    {
        public string ConfigPath { get; set; } = "/Failing";
        public List<string> EndpointPaths { get; set; } = new List<string>();
    }
}