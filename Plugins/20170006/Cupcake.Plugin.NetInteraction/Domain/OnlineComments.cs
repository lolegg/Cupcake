using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
    //网上征询
    public class OnlineComments : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //背景介绍
       // public string BackgroundIntroduction { get; set; }
        //草案全文
        //public string DraftFullText { get; set; }
        ////公众意见与建议
        //public string Opinion { get; set; }
        ////公众意见采纳情况反馈
        //public string Feedback { get; set; }
        //是否结束
        public bool IsEnd { get; set; }


        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    public class OnlineCommentsCondition : Condition
    {
        public string Title { get; set; }
    }
}
