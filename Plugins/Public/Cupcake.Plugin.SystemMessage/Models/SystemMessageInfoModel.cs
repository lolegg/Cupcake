using Cupcake.Core.Model;
using Cupcake.Plugin.SystemMessage.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.SystemMessage.Models
{
  public  class SystemMessageInfoModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
    }


  public class SystemMessageInfoListModel : ListModel<SystemMessageInfoModel>
  {
      public string Title { get; set; }



      public SystemMessageInfoListModel(IList<SystemMessageInfo> SystemMessageInfoItemList)
      {
          List = new List<SystemMessageInfoModel>();

          foreach (var item in SystemMessageInfoItemList)
          {
              SystemMessageInfoModel SystemMessageInfoModel = new SystemMessageInfoModel();
              SystemMessageInfoModel.Content = item.Content;
          
              SystemMessageInfoModel.Title = item.Title;
              SystemMessageInfoModel.Id = item.Id;


              List.Add(SystemMessageInfoModel);
          }

      }

  }
}
