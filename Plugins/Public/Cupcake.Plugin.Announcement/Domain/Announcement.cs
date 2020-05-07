using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Announcement.Domain
{
    public class AnnouncementInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int IsPlaced { get; set; }

        public string Name { get; set; }
    
        public string Department { get; set; }

        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
    }
    public class AnnouncementCondition : Condition
    {
        public string Title { get; set; }
    }
}
