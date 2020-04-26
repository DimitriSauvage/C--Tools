using System.Collections.Generic;

namespace DimitriSauvageTools.Mvc.ExtensionsInfrastructure.Abstractions
{
    public interface IExtensionMetadata
    {
        IEnumerable<StyleSheet> StyleSheets { get; }
        IEnumerable<Script> Scripts { get; }
        IEnumerable<MenuItem> MenuItems { get; }
    }
}
