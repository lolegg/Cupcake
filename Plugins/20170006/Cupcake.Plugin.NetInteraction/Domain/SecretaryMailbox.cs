using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
    //局长信箱
   public class SecretaryMailbox : PluginBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
       //答复
        public string Reply { get; set; }
       //是否显示到前端页面
        public bool IsDisplay { get; set; }

       //序列号
        public int serialNumber { get; set; }
       //反馈时间
        public DateTime? FeedbackTime { get; set; }
       //验证码
        public string YanZhengMa { get; set; }
    }
   public class SecretaryMailboxCondition : Condition
   {
       public string Title { get; set; }
   }
}
