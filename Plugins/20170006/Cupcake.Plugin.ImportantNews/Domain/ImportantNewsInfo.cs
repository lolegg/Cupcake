using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.ImportantNews.Domain
{
    public class ImportantNewsInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid NewsType { get; set; }

        public DateTime? ReleaseTime { get; set; }
    }
    public class NewsCondition : Condition
    {
        public string Title { get; set; }
        public Guid? NewsType { get; set; }
    }
}
