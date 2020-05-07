using Cupcake.Data.Mappings;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.PublicService.Data
{
    public partial class TipsForConvenienceMap : PluginBaseEntityMapping<TipsForConvenienceInfo>
    {
        public TipsForConvenienceMap()
        {
            this.ToTable("PublicService_TipsForConvenience");
        }
    }
}