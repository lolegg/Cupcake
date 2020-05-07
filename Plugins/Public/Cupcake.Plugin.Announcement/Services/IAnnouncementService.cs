using System;
namespace Cupcake.Plugin.Announcement.Services
{
    public interface IAnnouncementService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Announcement.Domain.AnnouncementInfo> SearchNotice(Cupcake.Plugin.Announcement.Domain.AnnouncementCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.Announcement.Domain.AnnouncementInfo googleProductRecord);
        System.Collections.Generic.IList<Cupcake.Plugin.Announcement.Domain.AnnouncementInfo> GetAll();
        Cupcake.Plugin.Announcement.Domain.AnnouncementInfo GetById(Guid googleProductRecordId);
        void InsertGoogleProductRecord(Cupcake.Plugin.Announcement.Domain.AnnouncementInfo googleProductRecord);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Announcement.Domain.AnnouncementInfo googleProductRecord);
    }
}
