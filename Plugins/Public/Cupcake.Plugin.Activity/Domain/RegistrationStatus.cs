using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Domain
{
    public class RegistrationStatus : PluginBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? SubordinateActivitiesID { get; set; }
        public string RegistrationName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegistrationTel { get; set; }
    }

    public class RegistrationStatusCondition : Condition
    {
        public string RegistrationName { get; set; }
        public Guid? SubordinateActivitiesID { get; set; }
    }
}
