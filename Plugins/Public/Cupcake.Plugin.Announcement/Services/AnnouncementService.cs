using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Announcement.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Cupcake.Core.Domain;
using System.Linq.Expressions;
using Cupcake.Plugin.Announcement.Models;

namespace Cupcake.Plugin.Announcement.Services
{
    public partial class AnnouncementService : IAnnouncementService
    {
        private readonly IRepository<AnnouncementInfo> repository;
        public AnnouncementService(IRepository<AnnouncementInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<AnnouncementInfo> SearchNotice(AnnouncementCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.BeginDate != null)
                query = query.Where(t => t.CreateDate > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.CreateDate < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);

            query = query.OrderByDescending(t => t.IsPlaced).ThenByDescending(t=>t.CreateDate);

            return new PagedList<AnnouncementInfo>(query, condition.PageIndex, condition.PageSize);
        }


        public virtual void DeleteGoogleProduct(AnnouncementInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");
            repository.Delete(googleProductRecord);
        }

        public virtual IList<AnnouncementInfo> GetAll()
        {
            var query = from gp in repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public virtual AnnouncementInfo GetById(Guid googleProductRecordId)
        {
            if (googleProductRecordId == null)
                return null;

            return repository.GetById(googleProductRecordId);
        }
        public virtual void InsertGoogleProductRecord(AnnouncementInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Insert(googleProductRecord);
        }

        public virtual void UpdateGoogleProductRecord(AnnouncementInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Update(googleProductRecord);
        }
    }
}
