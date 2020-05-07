using Cupcake.Data.Mappings;
using Cupcake.Plugin.Activity.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Activity.Data
{
    public partial class ActivitysMap : PluginBaseEntityMapping<Activitys>
    {
        public ActivitysMap()
        {
            this.ToTable("Activity_Activitys");
        }
    }
}