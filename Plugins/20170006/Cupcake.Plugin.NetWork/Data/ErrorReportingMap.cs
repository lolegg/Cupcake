using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetWork.Data
{
    public partial class ErrorReportingMap : PluginBaseEntityMapping<ErrorReporting>
    {
        public ErrorReportingMap()
        {
            this.ToTable("NetWork_ErrorReporting");
        }
    }
}