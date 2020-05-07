using Cupcake.Core.Domain;
using System;
namespace Cupcake.Plugin.MessageOpen.Domain
{
    //机构职能
   public class MessageOpenInstitutionalInfo:PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }

        public Guid? Institutional { get; set; }
        public string Department { get; set; }

    }
   public class MessageOpenInstitutionalCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }
       public DateTime? publicTime { get; set; }
       public Guid? Institutional { get; set; }
   }
}
