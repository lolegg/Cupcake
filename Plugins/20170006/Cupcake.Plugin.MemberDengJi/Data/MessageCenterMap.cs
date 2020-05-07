using Cupcake.Data.Mappings;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MemberDengJi.Data
{
    public partial class MessageCenterMap : PluginBaseEntityMapping<MessageCenter>
    {
        public MessageCenterMap()
        {
            this.ToTable("MemberDengJi_MessageCenter");
        }
    }
}