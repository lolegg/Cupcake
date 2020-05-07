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
    class MessageManagementsModel : BaseModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "所属活动ID")]
        public Guid? SubordinateActivitiesID { get; set; }
        [Display(Name = "留言人")]
        public string MessageName { get; set; }
        [Display(Name = "留言内容")]
        public string MessageConent { get; set; }
        [Display(Name = "留言时间")]
        public DateTime? MessageDate { get; set; }
        public MessageManagements ToInfo()
        {
            MessageManagements info = new MessageManagements();
            info.MessageName = this.MessageName;
            info.MessageConent = this.MessageConent;
            info.MessageDate = this.MessageDate;
            return info;
        }
        public MessageManagementsModel ToModel(MessageManagements info)
        {
            this.Id = info.Id;
            this.MessageName = info.MessageName;
            this.MessageConent = info.MessageConent;
            this.MessageDate = info.MessageDate;
            return this;
        }
        public MessageManagements FormData(MessageManagements info)
        {
            info.Id = this.Id;
            info.MessageName = this.MessageName;
            info.MessageConent = this.MessageConent;
            info.MessageDate = this.MessageDate;
            return info;
        }
    }
}
