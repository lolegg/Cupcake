using Cupcake.Data.Mappings;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;


namespace Cupcake.Plugin.MessageOpen.Data
{
    public class MessageOpenNormativeFileMap : PluginBaseEntityMapping<MessageOpenNormativeFileInfo>
    {
        public MessageOpenNormativeFileMap()
        {
            this.ToTable("MessageOpenNormativeFile");
        }
    }
}
