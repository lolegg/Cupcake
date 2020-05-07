using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MessageOpen.Domain
{
    //行政许可事项目录
    public class MessageOpenAdministrativeIndexInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }
        public string Department { get; set; }
        public bool IsTop { get; set; }
    }
   public class MessageOpenAdministrativeIndexCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }

       public DateTime? publicTime { get; set; }
       public Guid? IsTop { get; set; }

       public string Department { get; set; }
   }
}
