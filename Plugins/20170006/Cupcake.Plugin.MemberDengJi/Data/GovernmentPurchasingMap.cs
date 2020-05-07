using Cupcake.Data.Mappings;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MemberDengJi.Data
{
    public partial class GovernmentPurchasingMap : PluginBaseEntityMapping<GovernmentPurchasing>
    {
        public GovernmentPurchasingMap()
        {
            this.ToTable("MemberDengJi_GovernmentPurchasing");
        }
    }
}