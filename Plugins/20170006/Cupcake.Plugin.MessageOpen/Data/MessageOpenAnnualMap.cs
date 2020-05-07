using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;


namespace Cupcake.Plugin.MessageOpen.Data
{
    public class MessageOpenAnnualMap : PluginBaseEntityMapping<MessageOpenAnnualInfo>
    {
      public MessageOpenAnnualMap()
        {
            this.ToTable("MessageOpenAnnual");
        }
    }
}
