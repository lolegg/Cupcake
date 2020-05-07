using Cupcake.Core.Domain;
using System;
using Cupcake.Plugin.MessageOpen.Helper;

namespace Cupcake.Plugin.MessageOpen.Domain
{
    //政府信息公开  /规定/目录/指南/申请/
    public class MessageOpenInfo:PluginBaseEntity
    {
        public string Title { get; set; }
        public string publicName { get; set; }
        public string Content { get; set; }
        public DateTime? publicTime { get; set; }

        public Guid? MessageOpenChoose{get;set;}

        public string Catalog { get; set; }
        
        public string Department { get; set; }
    }
     public class MessageOpenCondition : Condition
   {
       public string Title { get; set; }
       public string publicName { get; set; }

       public DateTime? publicTime { get; set; }

       public Guid? MessageOpenChoose { get; set; }
   }
}
