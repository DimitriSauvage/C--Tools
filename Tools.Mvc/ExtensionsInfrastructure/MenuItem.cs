using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Mvc.ExtensionsInfrastructure
{
    public class MenuItem
    {
        public string Url { get; set; }
        public string Name { get; }
        public int Position { get; }

        public MenuItem(string url, string name, int position)
        {
            this.Url = url;
            this.Name = name;
            this.Position = position;
        }
    }
}
