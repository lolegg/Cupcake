using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.NetWork.Services
{
    public partial class OnlineAcceptanceService : IOnlineAcceptanceService
    {
        private readonly IRepository<OnlineAcceptance> repository;
        public OnlineAcceptanceService(IRepository<OnlineAcceptance> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(OnlineAcceptance OnlineAcceptance)
        {
            if (OnlineAcceptance == null)
                throw new ArgumentNullException("OnlineAcceptance");

            repository.Delete(OnlineAcceptance);
        }

        public virtual void InsertGoogleProductRecord(OnlineAcceptance OnlineAcceptance)
        {
            if (OnlineAcceptance == null)
                throw new ArgumentNullException("OnlineAcceptance");

            repository.Insert(OnlineAcceptance);
        }

        public virtual void UpdateGoogleProductRecord(OnlineAcceptance OnlineAcceptance)
        {
            if (OnlineAcceptance == null)
                throw new ArgumentNullException("OnlineAcceptance");

            repository.Update(OnlineAcceptance);
        }

        public IPagedList<OnlineAcceptance> SearchOnlineAcceptance(OnlineAcceptanceCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.WorkType != null)
                query = query.Where(t => t.WorkType == condition.WorkType);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineAcceptance>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual OnlineAcceptance GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }
    }
}
