using Cupcake.Plugin.Activity.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Models
{
    public class RegistrationStatusModel : BaseModel
    {
        public Guid UserId { get; set; }
        [Display(Name = "所属活动ID")]
        public Guid? SubordinateActivitiesID { get; set; }
        [Display(Name = "报名人")]
        public string RegistrationName { get; set; }
        [Display(Name = "报名时间")]
        public DateTime? RegistrationDate { get; set; }
        [Display(Name = "报名人电话")]
        public string RegistrationTel { get; set; }

        public RegistrationStatus ToInfo()
        {
            RegistrationStatus info = new RegistrationStatus();
            info.UserId = this.UserId;
            info.SubordinateActivitiesID = this.SubordinateActivitiesID;
            info.RegistrationName = this.RegistrationName;
            info.RegistrationDate = this.RegistrationDate;
            info.RegistrationTel = this.RegistrationTel;
            return info;
        }

        public RegistrationStatusModel ToModel(RegistrationStatus info)
        {
            this.Id = info.Id;
            this.UserId = info.UserId;
            this.SubordinateActivitiesID = info.SubordinateActivitiesID;
            this.RegistrationName = info.RegistrationName;
            this.RegistrationDate = info.RegistrationDate;
            this.RegistrationTel = info.RegistrationTel;
            return this;
        }

        public RegistrationStatus FormData(RegistrationStatus info)
        {
            info.Id = this.Id;
            info.UserId = this.UserId;
            info.SubordinateActivitiesID = this.SubordinateActivitiesID;
            info.RegistrationName = this.RegistrationName;
            info.RegistrationDate = this.RegistrationDate;
            info.RegistrationTel = this.RegistrationTel;
            return info;
        }
    }
}
