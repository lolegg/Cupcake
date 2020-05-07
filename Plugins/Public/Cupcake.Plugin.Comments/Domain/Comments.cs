using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Comments.Domain
{
    //评论表
    public class CommentsInfo : PluginBaseEntity
    {
        //用户头像
        public string ImgUrl { get; set; }
        //用户id
        public Guid UserId { get; set; }

        //用户名
        public string Name { get; set; }

        //回复内容
        public string Content { get; set; }

        //回复时间
        public DateTime PublicDate { get; set; }

        //所属信息id
        public Guid? ChildId { get; set; }

        //所属信息名称
        public string MessageName { get; set; }


        //被评论信息类型

        public Guid? TypeChildId { get; set; }

        //点赞数
        public int PraiseNum { get; set; }

        //回复数量
        public int ReplyNum { get; set; }


        //所属评论Id
        public Guid? ToCommentsId { get; set; }

        //二级评论所属信息
        public Guid? ToTwoCommentsId { get; set; }
        
      
    }
    public class CommentsCondition : Condition
    {
        public Guid? ChildId { get; set; }
        public Guid ToCommentsId { get; set; }
    }
}
