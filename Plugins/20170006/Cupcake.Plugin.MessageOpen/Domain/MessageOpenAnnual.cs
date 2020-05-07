using Cupcake.Core.Domain;
using System;


namespace Cupcake.Plugin.MessageOpen.Domain
{
    public class MessageOpenAnnualInfo : PluginBaseEntity
    {
        //信息公开年报
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }
        public string Department { get; set; }
        public bool IsTop { get; set; }
    }
   public class MessageOpenAnnualCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }
       public DateTime? publicTime { get; set; }
       public Guid? IsTop { get; set; }
       public string Department { get; set; }
   }
}
