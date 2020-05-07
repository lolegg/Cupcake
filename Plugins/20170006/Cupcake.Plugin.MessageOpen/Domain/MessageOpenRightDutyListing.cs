using Cupcake.Core.Domain;
using System;


namespace Cupcake.Plugin.MessageOpen.Domain
{
    //权利清单和责任清单
    public class MessageOpenRightDutyListingInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }
        public string Department { get; set; }
        public bool IsTop { get; set; }
    }
   public class MessageOpenRightDutyListingCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }

       public DateTime? publicTime { get; set; }
       public Guid? IsTop { get; set; }
       public string Department { get; set; }
   }
}
