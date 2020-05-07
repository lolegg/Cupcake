using Cupcake.Data.Mappings;
using Cupcake.Plugin.Announcement.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Announcement.Data
{
    public partial class AnnouncementMap : PluginBaseEntityMapping<AnnouncementInfo>
    {
        public AnnouncementMap()
        {
            this.ToTable("Announcement");
        }
    }
}