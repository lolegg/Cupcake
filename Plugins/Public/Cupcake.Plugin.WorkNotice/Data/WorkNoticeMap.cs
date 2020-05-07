using Cupcake.Data.Mappings;
using Cupcake.Plugin.WorkNotice.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.WorkNotice.Data
{
    public partial class WorkNoticeMap : PluginBaseEntityMapping<WorkNotices>
    {
        public WorkNoticeMap()
        {
            this.ToTable("WorkNotice_WorkNotices");
        }
    }
}