using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Comments.Domain
{
    //收藏表
    public class Comments_CollectInfo : PluginBaseEntity
    {
      //用户Id
      public Guid UserId { get; set; }

      //被收藏信息Id
      public Guid ChildId { get; set; }
      
      //收藏的信息标题
      public string Title { get; set; }
       
    }
}
