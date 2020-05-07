using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
    //在线访谈
    public class OnlineInterview : PluginBaseEntity
    {
        public string theme { get; set; }//主题
        public string Guest { get; set; }//嘉宾
        public DateTime? shijian { get; set; }//时间
        public string Address { get; set; }//地点
        //是否开启网友提问
        public bool IsEnabled { get; set; } 

    }

    public class OnlineInterviewCondition : Condition
    {
        public string Title { get; set; }


    }
}
