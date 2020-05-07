using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetWork.Models
{
   public  class OnlineAcceptanceModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "所属类型")]
        public Guid? WorkType { get; set; }
        [Display(Name = "跳转链接")]
        public string JumpUrl { get; set; }
        public OnlineAcceptance ToInfo()
        {
            OnlineAcceptance info = new OnlineAcceptance();
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.JumpUrl = this.JumpUrl;
            return info;
        }

        public OnlineAcceptanceModel ToModel(OnlineAcceptance info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.WorkType = info.WorkType;
            this.JumpUrl = info.JumpUrl;
            return this;
        }

        public OnlineAcceptance FormData(OnlineAcceptance info)
        {
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.JumpUrl = this.JumpUrl;
            return info;
        }
    }
}
