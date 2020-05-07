using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetWork.Data
{
    public partial class WorkGuideMap : PluginBaseEntityMapping<WorkGuide>
    {
        public WorkGuideMap()
        {
            this.ToTable("NetWork_WorkGuide");
        }
    }
}