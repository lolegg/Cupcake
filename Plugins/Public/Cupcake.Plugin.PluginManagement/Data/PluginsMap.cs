using Cupcake.Data.Mappings;
using Cupcake.Plugin.PluginManagement.Domain;

namespace Cupcake.Plugin.PluginManagement.Data
{
    public partial class PluginsMap : PluginBaseEntityMapping<Plugins>
    {
        public PluginsMap()
        {
            this.ToTable("PluginManagement_Plugins");
        }
    }
}