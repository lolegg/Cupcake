using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Cupcake.Web.WapTemplate.edmx;

namespace Cupcake.Web.WapTemplate.Models
{
    public class CommentsModel
    {
        public CommentsModel()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
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

        public Guid TypeChildId { get; set; }

        //点赞数
        public int PraiseNum { get; set; }

        //所属评论Id
        public Guid? ToCommentsId { get; set; }

        //二级评论所属信息
        public Guid? ToTwoCommentsId { get; set; }
        //评论List
        public List<CommentsModel> ListCommentsModel { get; set; }

        //评论点赞数List
        public List<Comments_PraiseModel> ListComments_PraiseModel { get; set; }


    
        //flag

           //总行数
            public int PageTotal { get; set; }
            //当前页
            public int PageIndex { get; set; }
            //
            public int PageSize { get; set; }
        
    }

}