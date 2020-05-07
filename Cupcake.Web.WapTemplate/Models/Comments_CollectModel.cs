using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.WapTemplate.Models
{
    public class Comments_CollectModel
    {
        public Comments_CollectModel()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        //用户Id
        public Guid UserId { get; set; }

        //被收藏Id
        public Guid ChildId { get; set; }
        //收藏的信息标题
        public string Title { get; set; }

        public List<Comments_CollectModel> ListComments_CollectModel { get; set; }
    }
}