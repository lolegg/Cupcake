using Cupcake.Core.Domain;
using Cupcake.Plugin.WorkNotice.Domain;
using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.WorkNotice.Services
{
    public interface IWorkNoticeService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.WorkNotice.Domain.WorkNotices> SearchWorkNotices(Cupcake.Plugin.WorkNotice.Domain.WorkNoticeCondition condition);

        void DeleteGoogleProduct(Cupcake.Plugin.WorkNotice.Domain.WorkNotices WorkNotices);
        void InsertGoogleProductRecord(Cupcake.Plugin.WorkNotice.Domain.WorkNotices WorkNotices);
        void UpdateGoogleProductRecord(Cupcake.Plugin.WorkNotice.Domain.WorkNotices WorkNotices);
        WorkNotices GetById(Guid id);
    }
}
