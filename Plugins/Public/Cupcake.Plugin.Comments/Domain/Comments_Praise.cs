using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Comments.Domain
{
    //点赞表
    public class Comments_PraiseInfo : PluginBaseEntity
    {
        //用户id
        public Guid UserId { get; set; }
        
        //被点赞id
        public Guid ChildId { get; set; }

        //点赞次数
        public int Praisenum { get; set; }

    }
    public class Comments_PraiseCondition : Condition
    {
    
    
    }
}
