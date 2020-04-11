using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Mvc.ExtensionsInfrastructure.Abstractions
{
    public interface IExtensionMetadata
    {
        IEnumerable<StyleSheet> StyleSheets { get; }
        IEnumerable<Script> Scripts { get; }
        IEnumerable<MenuItem> MenuItems { get; }
    }
}
