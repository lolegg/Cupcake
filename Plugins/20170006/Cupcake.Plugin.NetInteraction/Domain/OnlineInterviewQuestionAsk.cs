using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
    public class OnlineInterviewQuestionAsk : PluginBaseEntity
    {
        public Guid OnlineInterviewId { get; set; }
        public string Question { get; set; }
        public string Ask { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    public class OnlineInterviewQuestionAskCondition : Condition
    {
        public string Title { get; set; }
        public Guid OnlineInterviewId { get; set; }
    }
}
