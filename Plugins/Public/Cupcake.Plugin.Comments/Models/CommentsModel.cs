using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.Comments.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.Comments.Models
{
    public class CommentsModel : BaseModel
    {
        //用户头像
        public string ImgUrl { get; set; }
        //用户id
        public Guid UserId { get; set; }
        [Required]
        [Display(Name = "用户名")]
        //用户名
        public string Name { get; set; }
        [Required]
        [Display(Name = "回复内容")]
        //回复内容
        public string Content { get; set; }
        [Required]
        [Display(Name = "回复时间")]
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

        //所属信息Id
        public Guid? ToMessageId { get; set; }

        //所属评论Id
        public Guid ToCommentsId { get; set; }


        //二级评论所属信息
        public Guid? ToTwoCommentsId { get; set; }

        //评论List
        public List<CommentsModel> ListCommentsModel { get; set; }

        //评论点赞数List
        public List<Comments_PraiseModel> ListComments_PraiseModel { get; set; }
    }
}
