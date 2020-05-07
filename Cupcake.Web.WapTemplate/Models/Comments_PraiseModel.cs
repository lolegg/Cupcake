using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class Comments_PraiseModel
    {  //用户id
        public Guid UserId { get; set; }

        //被点赞id
        public Guid ChildId { get; set; }

    }
}