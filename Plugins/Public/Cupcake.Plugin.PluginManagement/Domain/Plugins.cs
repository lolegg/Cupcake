using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.PluginManagement.Domain
{
    public class Plugins : PluginBaseEntity
    {
        public string Group { get; set; }
        public string SystemName { get; set; }
        public string FriendlyName { get; set; }
        public int BigVersion { get; set; }
        public int SmallVersion { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Guid PluginType { get; set; }
    }
    public class PluginsCondition : Condition
    {
        public string SystemName { get; set; }
        public Guid? PluginType { get; set; }
    }
}
