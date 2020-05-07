using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Comments.Models
{
   public class Comments_PraiseModel
    {
        //用户id
        public Guid UserId { get; set; }

        //被点赞id
        public Guid ChildId { get; set; }

        //点赞次数
        public int Praisenum { get; set; }
    }
}
