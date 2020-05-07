using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MemberDengJi.Domain
{
    public class MessageCenter : PluginBaseEntity
    {
        public Guid? NewsId { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
        public string MessageType { get; set; }
        public bool Type { get; set; }
    }
    
}
