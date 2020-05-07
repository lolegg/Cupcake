using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MessageOpen.Domain
{
    //通知公告
    public class MessageOpenAnnouncementsInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }
        public string Department { get; set; }
        public bool IsTop { get; set; }
        public Guid? MessageOpenType { get; set; }
    }
  public class MessageOpenAnnouncementsCondition : Condition
  {
      public string Title { get; set; }
      public string publicName { get; set; }
      public DateTime? publicTime { get; set; }
      public Guid? IsTop { get; set; }
      public string Department { get; set; }

      public Guid? MessageOpenType { get; set; }
  }
}
