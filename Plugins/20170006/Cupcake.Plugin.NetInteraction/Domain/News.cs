using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Template.Domain
{
    public class News : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid NewsType { get; set; }
    }
    public class NewsCondition : Condition
    {
        public string Title { get; set; }
        public Guid? NewsType { get; set; }
    }
}
