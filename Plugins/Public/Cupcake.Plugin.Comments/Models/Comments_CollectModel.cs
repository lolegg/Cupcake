using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.Comments.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.Comments.Models
{
   public class Comments_CollectModel:BaseModel
    {
        //用户Id
        public Guid UserId { get; set; }

        //被收藏Id
        public Guid ChildId { get; set; }
        //收藏的信息标题
        public string Title { get; set; }
    }
}
