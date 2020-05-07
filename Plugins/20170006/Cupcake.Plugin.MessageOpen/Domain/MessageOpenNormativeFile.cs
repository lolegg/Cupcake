using Cupcake.Core.Domain;
using System;
namespace Cupcake.Plugin.MessageOpen.Domain
{
    //规范性文件
    public class MessageOpenNormativeFileInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }
        public string Department { get; set; }
        public bool IsTop { get; set; }
    }
   public class MessageOpenNormativeFileCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }
       public DateTime? publicTime { get; set; }
       public Guid? IsTop { get; set; }
       public string Department { get; set; }
   }
}
