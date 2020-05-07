using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MapMessage.Domain
{
    public class MapMessageInfo : PluginBaseEntity
    {
        public string Name { get; set; }
     /// <summary>
     /// 类别
     /// </summary>
        public Guid SortType { get; set; }
        public DateTime? EstablishDate { get; set; }

        public Guid RegisterOrganization { get; set; }
        /// <summary>
        /// 所属模块 
        /// </summary>
        public Guid SortModual { get; set; }

        public string Address { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }




    }
    public class MapMessageCondition : Condition
    {
        public string Name { get; set; }
        public Guid? SortModual { get; set; }
    }
}
