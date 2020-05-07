using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MessageOpen.Domain
{
    public class MessageOpenMechanismInfo:PluginBaseEntity
    {
        //名称
        public string Name { get; set; }
        //电话
        public string Phone { get; set; }
        //邮箱
        public string Mailbox { get; set; }
        //地址
        public string Address { get; set; }
        //接待时间
        public string MeetTime { get; set; }
        //发布人
        public string publicName { get; set; }
    }
    public class MessageOpenMechanismCondition : Condition
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
