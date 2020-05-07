using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.NetWork.Services
{
    public partial class ErrorReportingService : IErrorReportingService
    {
        private readonly IRepository<ErrorReporting> repository;
        private readonly IRepository<FormDownload> repository1;
        public ErrorReportingService(IRepository<ErrorReporting> repository, IRepository<FormDownload> repository1)
        {
            this.repository = repository;
            this.repository1 = repository1;
        
        }

        public virtual void DeleteGoogleProduct(ErrorReporting ErrorReporting)
        {
            if (ErrorReporting == null)
                throw new ArgumentNullException("ErrorReporting");

            repository.Delete(ErrorReporting);
        }

        public virtual void InsertGoogleProductRecord(ErrorReporting ErrorReporting)
        {
            if (ErrorReporting == null)
                throw new ArgumentNullException("ErrorReporting");

            repository.Insert(ErrorReporting);
        }

        public virtual void UpdateGoogleProductRecord(ErrorReporting ErrorReporting)
        {
            if (ErrorReporting == null)
                throw new ArgumentNullException("ErrorReporting");

            repository.Update(ErrorReporting);
        }

        public IPagedList<ErrorReporting> SearchErrorReporting(ErrorReportingCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.TheTitle != null)
                query = query.Where(t => t.TheTitle == condition.TheTitle);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<ErrorReporting>(query, condition.PageIndex, condition.PageSize);
        }
        public virtual ErrorReporting GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }
        public string GetByType(Guid? Id) 
        {
            return repository1.TableNoTracking.Where(n => n.Id == Id).FirstOrDefault().Title;
        }

    }
}
