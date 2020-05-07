using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MessageOpen.Data
{
    public class MessageOpenAdministrativePunishMap : PluginBaseEntityMapping<MessageOpenAdministrativePunishInfo>
    {
       public MessageOpenAdministrativePunishMap()
        {
            this.ToTable("MessageOpenAdministrativePunish");
        }
    }
}
