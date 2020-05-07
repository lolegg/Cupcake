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
    public partial class CommentsService : ICommentsService
    {
        private readonly IRepository<CommentsInfo> repository;
        public CommentsService(IRepository<CommentsInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<CommentsInfo> SearchNotice(CommentsCondition condition)
        {
            var query = repository.Table;
            if (condition.BeginDate != null)
                query = query.Where(t => t.CreateDate > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.CreateDate < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
           query = query.Where(t => t.ChildId==condition.ChildId);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<CommentsInfo>(query, condition.PageIndex, condition.PageSize);
        }

        public IPagedList<CommentsInfo> ChildidSearchNotice(CommentsCondition condition) 
        {
            var query = repository.Table;
            if (condition.BeginDate != null)
                query = query.Where(t => t.CreateDate > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.CreateDate < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
            Guid falseid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            query = query.Where(t => t.ChildId == falseid);
            query = query.Where(t => t.ToCommentsId == condition.ToCommentsId);
            query = query.OrderByDescending(t => t.CreateDate);
            return new PagedList<CommentsInfo>(query, condition.PageIndex, condition.PageSize);
        }


        public virtual int DeleteGoogleProduct(CommentsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");
                
                repository.Delete(googleProductRecord);
                var result = 1;
                return result;
        }

        public virtual IList<CommentsInfo> GetAll()
        {
            var query = from gp in repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public virtual CommentsInfo GetById(Guid googleProductRecordId)
        {
            if (googleProductRecordId == null)
                return null;

            return repository.GetById(googleProductRecordId);
        }
        public virtual int GetByToCommentsId(Guid googleProductRecordId) 
        {
            if (googleProductRecordId==null)
            {
                return -1;
            }
             var query = repository.Table;
             return query.Where(m => m.ToCommentsId == googleProductRecordId && m.IsDelete == false).Count();
        }

        public virtual void InsertGoogleProductRecord(CommentsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Insert(googleProductRecord);
        }

        public virtual void UpdateGoogleProductRecord(CommentsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Update(googleProductRecord);
        }

        public virtual int UpdateReplyNum(Guid googleProductRecordId)
        {
            if (googleProductRecordId==null)
                throw new ArgumentNullException("googleProductRecord");
            var query = repository.Table;
            var list= query.Where(m => m.Id == googleProductRecordId && m.IsDelete == false).SingleOrDefault();
            list.ReplyNum = list.ReplyNum - 1;
            repository.Update(list);
            var result=1;
            return result;
        }
    }
}
