using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Activity.Domain
{
    public class Activitys : PluginBaseEntity
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public Guid? ActityType { get; set; }
        public Guid? ActityState { get; set; }
        public string Imgurl { get; set; }
        public string Conent { get; set; }
        public bool IsTop { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Dimension { get; set; }
    }
    public class ActivityCondition : Condition
    {
        public string Title { get; set; }
    }
}
