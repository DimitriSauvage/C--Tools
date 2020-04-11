using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;

namespace Tools.Mvc.Razor
{
    public class PluginViewLocationExpander : IViewLocationExpander
    {
        #region Constantes
        private const string PLUGIN_KEY = "plugin";
        private const string THEME_KEY = "theme";
        #endregion

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(PLUGIN_KEY, out string plugin);
            context.Values.TryGetValue(THEME_KEY, out string theme);

            if(!string.IsNullOrWhiteSpace(theme) && !string.Equals(theme, "Generic", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(plugin))
                {
                    var pluginViewLocations = new string[]
                    {
                        $"/Themes/{theme}/Plugins/{plugin}/Views/{{1}}/{{0}}.cshtml",
                        $"/Themes/{theme}/Plugins/{plugin}/Views/Shared/{{0}}.cshtml",
                        $"/Plugins/{plugin}/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/{plugin}/Views/Shared/{{0}}.cshtml",
                        $"/Themes/{theme}/Views/Shared/{{0}}.cshtml"
                    };

                    viewLocations = pluginViewLocations.Concat(viewLocations);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(plugin))
                {
                    var moduleViewLocations = new string[]
                    {
                        $"/Plugins/{plugin}/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/{plugin}/Views/Shared/{{0}}.cshtml"
                    };

                    viewLocations = moduleViewLocations.Concat(viewLocations);
                }
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controllerName = context.ActionContext.ActionDescriptor.DisplayName;
            if (controllerName == null) // in case of render view to string
            {
                return;
            }

            // Get assembly name
            var moduleName = controllerName.Split('(', ')')[1];
            context.Values[PLUGIN_KEY] = moduleName;

            context.ActionContext.HttpContext.Request.Cookies.TryGetValue("theme", out string previewingTheme);
            if (!string.IsNullOrWhiteSpace(previewingTheme))
            {
                context.Values[THEME_KEY] = previewingTheme;
            }
            else
            {
                var config = context.ActionContext.HttpContext.RequestServices.GetService<IConfiguration>();
                context.Values[THEME_KEY] = config["Theme"];
            }
        }
    }
}
