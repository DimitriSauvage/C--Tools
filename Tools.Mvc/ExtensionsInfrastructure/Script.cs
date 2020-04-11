using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Mvc.ExtensionsInfrastructure
{
    public class Script
    {
        public string Url { get; set; }
        public int Position { get; }

        public Script(string url, int position)
        {
            this.Url = url;
            this.Position = position;
        }
    }
}
