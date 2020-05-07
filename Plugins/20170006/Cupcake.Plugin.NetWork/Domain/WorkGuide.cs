using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.NetWork.Domain
{
    public class WorkGuide : PluginBaseEntity
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
        public string ScopeOfApplication { get; set; }
        public string CommitmentTimeLimit { get; set; }
        public string LegalTimeLimit { get; set; }
        public string HandlePlaceTime { get; set; }
        public string ApplicationConditions { get; set; }
        public string ApplicationMaterials { get; set; }
        public string SetBasis { get; set; }
        public string ManagementProcess { get; set; }
        public string Other { get; set; }
      
    }
    public class WorkGuideCondition : Condition
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
    }
}
