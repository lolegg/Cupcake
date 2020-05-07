using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using Cupcake.Plugin.TaskModule.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Services
{
    public partial class TaskDisposalService : PluginBaseService<TaskDisposalInfo>, ITaskDisposalService
    {
        public TaskDisposalService(IRepository<TaskDisposalInfo> repository)
            : base(repository)
        {
         
        }


        #region Methods

        //public virtual void DeleteGoogleProduct(TaskDisposalInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Delete(googleProductRecord);
        //}

        //public virtual IList<TaskDisposalInfo> GetAll()
        //{
        //    var query = from gp in _gpRepository.Table
        //                orderby gp.Id
        //                select gp;
        //    var records = query.ToList();
        //    return records;
        //}
        
        //public virtual TaskDisposalInfo GetById(object googleProductRecordId)
        //{
        //    if (googleProductRecordId==null)
        //        return null;

        //    return _gpRepository.GetById(googleProductRecordId);
        //}

        public virtual TaskDisposalInfo GetByTaskIssuedId(Guid taskIssuedId)
        {
            var query = from gp in repository.Table
                        where gp.TaskIssuedId == taskIssuedId
                        orderby gp.Id
                        select gp;
            var record = query.FirstOrDefault();
            return record;
        }
        //public virtual void InsertGoogleProductRecord(TaskDisposalInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Insert(googleProductRecord);
        //}

        //public virtual void UpdateGoogleProductRecord(TaskDisposalInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Update(googleProductRecord);
        //}
        //public virtual IList<TaskDisposalInfo> GetListByCondition(ref Paging paging, TaskDisposalCondition condition)
        //{
        //    Expression<Func<TaskDisposalInfo, bool>> filter = PredicateExtensions.True<TaskDisposalInfo>();
        //    filter = filter.And(u => u.IsDelete == false);
        //    if (condition.TaskIssuedId.HasValue) filter = filter.And(u => u.TaskIssuedId == condition.TaskIssuedId);
        //    return _gpRepository.GetPaged<DateTime>(ref paging, filter, o=>o.CreateDate, false).ToList();
        //}
        public IPagedList<TaskDisposalInfo> SearchTaskDisposal(TaskDisposalCondition condition)
        {
            var query = repository.Table;
            if (condition.TaskIssuedId.HasValue) query = query.Where(u => u.TaskIssuedId == condition.TaskIssuedId);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<TaskDisposalInfo>(query, condition.PageIndex, condition.PageSize);
        }
        #endregion
    }
}
