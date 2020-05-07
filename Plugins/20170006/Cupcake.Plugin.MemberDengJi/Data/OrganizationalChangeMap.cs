using Cupcake.Data.Mappings;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MemberDengJi.Data
{
    public partial class OrganizationalChangeMap : PluginBaseEntityMapping<OrganizationalChange>
    {
        public OrganizationalChangeMap()
        {
            this.ToTable("MemberDengJi_OrganizationalChange");
        }
    }
}