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
    public partial class Comments_CollectService
    {
        private readonly IRepository<Comments_CollectInfo> repository;
        public Comments_CollectService(IRepository<Comments_CollectInfo> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(Comments_CollectInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");
            repository.Delete(googleProductRecord);
        }

        public virtual IList<Comments_CollectInfo> GetAll()
        {
            var query = from gp in repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public virtual Comments_CollectInfo GetById(Guid googleProductRecordId)
        {
            if (googleProductRecordId == null)
                return null;

            return repository.GetById(googleProductRecordId);
        }
        public virtual void InsertGoogleProductRecord(Comments_CollectInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Insert(googleProductRecord);
        }

        public virtual void UpdateGoogleProductRecord(Comments_CollectInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Update(googleProductRecord);
        }
    }
}
