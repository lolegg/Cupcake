using System;

namespace Cupcake.Plugin.PluginManagement
{
    public static class PluginHelper
    {
        public static string GetViewPath(Type type, string viewName)
        {
            string controllerName = type.FullName.Substring(type.FullName.LastIndexOf(".") + 1).Replace("Controller", "");
            var nameSpace = type.Module.Name.Replace(".dll", "");

            return string.Format("~/Plugins/{0}/Views/{1}/{2}.cshtml", nameSpace, controllerName, viewName);
        }
    }
}
