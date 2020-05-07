using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class RegistrationStatusModel
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

    }
}