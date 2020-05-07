using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;


namespace Cupcake.Plugin.MessageOpen.Data
{
    public class MessageOpenRecentlyMap : PluginBaseEntityMapping<MessageOpenRecentlyInfo>
    {
        public MessageOpenRecentlyMap()
        {
            this.ToTable("MessageOpenRecently");
        }
    }
}
