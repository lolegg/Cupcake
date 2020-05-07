using Cupcake.Data.Mappings;
using Cupcake.Plugin.CalendarAndScheduling.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.CalendarAndScheduling.Data
{
    public partial class CalendarAndSchedulingMap : PluginBaseEntityMapping<CalendarAndSchedulingInfo>
    {
        public CalendarAndSchedulingMap()
        {
            this.ToTable("CalendarAndScheduling_Table");
        }
    }
}