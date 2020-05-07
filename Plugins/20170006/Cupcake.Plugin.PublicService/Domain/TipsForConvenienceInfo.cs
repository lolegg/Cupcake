using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.PublicService.Domain
{
    public class TipsForConvenienceInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class TipsForConvenienceCondition : Condition
    {
        public string Title { get; set; }

    }
}
