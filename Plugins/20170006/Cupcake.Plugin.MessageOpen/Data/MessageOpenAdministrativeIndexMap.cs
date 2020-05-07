using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MessageOpen.Data
{
   public class MessageOpenAdministrativeIndexMap : PluginBaseEntityMapping<MessageOpenAdministrativeIndexInfo>
    {
       public MessageOpenAdministrativeIndexMap()
        {
            this.ToTable("MessageOpenAdministrativeIndex");
        }
    }
}
