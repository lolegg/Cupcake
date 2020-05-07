using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.NetWork.Domain
{
    public class OnlineAcceptance : PluginBaseEntity
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
        public string JumpUrl { get; set; }
    }
    public class OnlineAcceptanceCondition : Condition
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
    }
}
