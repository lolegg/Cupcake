using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;


namespace Cupcake.Plugin.MessageOpen.Data
{
    public class MessageOpenRightDutyListingMap : PluginBaseEntityMapping<MessageOpenRightDutyListingInfo>
    {
        public MessageOpenRightDutyListingMap()
        {
            this.ToTable("MessageOpenRightDutyListing");
        }
    }
}
