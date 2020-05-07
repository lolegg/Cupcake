using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Comments.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Cupcake.Core.Domain;
using System.Linq.Expressions;
using Cupcake.Plugin.Comments.Models;

namespace Cupcake.Plugin.Comments.Services
{
    public partial class Comments_PraiseService : IComments_PraiseService
    {
        private readonly IRepository<Comments_PraiseInfo> repository;
        public Comments_PraiseService(IRepository<Comments_PraiseInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<Comments_PraiseInfo> SearchNotice(Comments_PraiseCondition condition)
        {
            var query = repository.Table;
            if (condition.BeginDate != null)
                query = query.Where(t => t.CreateDate > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.CreateDate < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);

            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Comments_PraiseInfo>(query, condition.PageIndex, condition.PageSize);
        }


        public virtual void DeleteGoogleProduct(Comments_PraiseInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");
            repository.Delete(googleProductRecord);
        }

        public virtual IList<Comments_PraiseInfo> GetAll()
        {
            var query = from gp in repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public virtual Comments_PraiseInfo GetById(Guid googleProductRecordId)
        {
            if (googleProductRecordId == null)
                return null;

            return repository.GetById(googleProductRecordId);
        }
        public virtual void InsertGoogleProductRecord(Comments_PraiseInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Insert(googleProductRecord);
        }

        public virtual void UpdateGoogleProductRecord(Comments_PraiseInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Update(googleProductRecord);
        }
    }
}
