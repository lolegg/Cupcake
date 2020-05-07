using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.News.Domain
{
    public class NewsInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid NewsType { get; set; }

        public string Name { get; set; }

        public DateTime publicTime { get; set; }
    }
    public class NewsCondition : Condition
    {
        public string Title { get; set; }
        public Guid? NewsType { get; set; }
    }
}
