using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public partial class MessageManagementsService : IMessageManagementsService
    {
        private readonly IRepository<MessageManagements> repository;
        public MessageManagementsService(IRepository<MessageManagements> repository)
        {
            this.repository = repository;
        }
        public virtual void DeleteGoogleProduct(MessageManagements MessageManagements)
        {
            if (MessageManagements == null)
                throw new ArgumentNullException("MessageManagements");

            repository.Delete(MessageManagements);
        }
        public virtual void InsertGoogleProductRecord(MessageManagements MessageManagements)
        {
            if (MessageManagements == null)
                throw new ArgumentNullException("MessageManagements");

            repository.Insert(MessageManagements);
        }

        public virtual void UpdateGoogleProductRecord(MessageManagements MessageManagements)
        {
            if (MessageManagements == null)
                throw new ArgumentNullException("MessageManagements");

            repository.Update(MessageManagements);
        }

        public IPagedList<MessageManagements> SearchMessageManagements(MessageManagementsCondition condition,Guid id)
        {
            var query = repository.Table;
            if (!string.IsNullOrEmpty(condition.MessageConent))
                query = query.Where(t => t.MessageConent.Contains(condition.MessageConent));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);
            query = query.Where(t => t.SubordinateActivitiesID == id);

            return new PagedList<MessageManagements>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual MessageManagements GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }
    }
}
