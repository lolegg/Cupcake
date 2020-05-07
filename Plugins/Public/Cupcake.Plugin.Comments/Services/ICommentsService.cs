using System;
using Cupcake.Core;
namespace Cupcake.Plugin.Comments.Services
{
    public interface ICommentsService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Comments.Domain.CommentsInfo> SearchNotice(Cupcake.Plugin.Comments.Domain.CommentsCondition condition);

        Cupcake.Core.IPagedList<Cupcake.Plugin.Comments.Domain.CommentsInfo> ChildidSearchNotice(Cupcake.Plugin.Comments.Domain.CommentsCondition condition);
        int DeleteGoogleProduct(Cupcake.Plugin.Comments.Domain.CommentsInfo googleProductRecord);
        System.Collections.Generic.IList<Cupcake.Plugin.Comments.Domain.CommentsInfo> GetAll();
        Cupcake.Plugin.Comments.Domain.CommentsInfo GetById(Guid googleProductRecordId);
        int GetByToCommentsId(Guid googleProductRecordId);
        void InsertGoogleProductRecord(Cupcake.Plugin.Comments.Domain.CommentsInfo googleProductRecord);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Comments.Domain.CommentsInfo googleProductRecord);

        int UpdateReplyNum(Guid googleProductRecordId);
    }
}
