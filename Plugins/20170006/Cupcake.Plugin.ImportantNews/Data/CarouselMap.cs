using Cupcake.Data.Mappings;
using Cupcake.Plugin.ImportantNews.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.ImportantNews.Data
{
    public  class CarouselMap : PluginBaseEntityMapping<CarouselInfo>
    {
        public CarouselMap()
        {
            this.ToTable("ImportantNews_Carousel");
        }
    }
}