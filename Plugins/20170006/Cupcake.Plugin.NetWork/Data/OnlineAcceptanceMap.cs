using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetWork.Data
{
    public partial class OnlineAcceptanceMap : PluginBaseEntityMapping<OnlineAcceptance>
    {
        public OnlineAcceptanceMap()
        {
            this.ToTable("NetWork_OnlineAcceptance");
        }
    }
}