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
    public partial class TaskIssuedService : PluginBaseService<TaskIssuedInfo>,ITaskIssuedService
    {
        #region Fields
        public TaskIssuedService(IRepository<TaskIssuedInfo> repository)
            : base(repository)
        {
         
        }

        //private readonly IRepository<TaskIssuedInfo> _gpRepository;

        //#endregion

        //#region Ctor

        //public TaskIssuedService(IRepository<TaskIssuedInfo> gpRepository)
        //{
        //    this._gpRepository = gpRepository;
        //}

        #endregion

        #region Methods

        //public virtual void DeleteGoogleProduct(TaskIssuedInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Delete(googleProductRecord);
        //}

        //public virtual IList<TaskIssuedInfo> GetAll()
        //{
        //    var query = from gp in _gpRepository.Table
        //                orderby gp.Id
        //                select gp;
        //    var records = query.ToList();
        //    return records;
        //}
        
        //public virtual TaskIssuedInfo GetById(object googleProductRecordId)
        //{
        //    if (googleProductRecordId==null)
        //        return null;

        //    return _gpRepository.GetById(googleProductRecordId);
        //}

        //public virtual TaskIssuedInfo GetByProductId(string productId)
        //{
        //    //    var query = from gp in _gpRepository.Table
        //    //                where gp.ItemGroupId == productId
        //    //                orderby gp.Id
        //    //                select gp;
        //    //    var record = query.FirstOrDefault();
        //    //    return record;
        //    return null;
        //}
        //public virtual void InsertGoogleProductRecord(TaskIssuedInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Insert(googleProductRecord);
        //}

        //public virtual void UpdateGoogleProductRecord(TaskIssuedInfo googleProductRecord)
        //{
        //    if (googleProductRecord == null)
        //        throw new ArgumentNullException("googleProductRecord");

        //    _gpRepository.Update(googleProductRecord);
        //}
        //public virtual IList<TaskIssuedInfo> GetListByCondition(ref Paging paging, TaskIssuedCondition condition)
        //{
        //    Expression<Func<TaskIssuedInfo, bool>> filter = PredicateExtensions.True<TaskIssuedInfo>();
        //    filter = filter.And(u => u.IsDelete == false);
        //    if (condition.IssuedDate.HasValue) filter = filter.And(u => u.IssuedDate == condition.IssuedDate);
        //    if (!string.IsNullOrEmpty(condition.TaskType)) filter = filter.And(u => u.TaskType == condition.TaskType);
        //    if (condition.TaskManagement.HasValue) filter = filter.And(u => u.TaskManagement == condition.TaskManagement);
        //    if (!string.IsNullOrEmpty(condition.TaskOverview)) filter = filter.And(u => u.TaskOverview.Contains(condition.TaskOverview));
        //    return _gpRepository.GetPaged<DateTime>(ref paging, filter, o=>o.CreateDate, false).ToList();
        //}
        public IPagedList<TaskIssuedInfo> SearchTaskIssued(TaskIssuedCondition condition)
        {
            var query = repository.Table;
            if (condition.IssuedDateStart.HasValue) query = query.Where(u => u.IssuedDate>= condition.IssuedDateStart);
            if (condition.IssuedDateEnd.HasValue) query = query.Where(u => u.IssuedDate <= condition.IssuedDateEnd);
            if (!string.IsNullOrEmpty(condition.TaskType)) query = query.Where(u => u.TaskType == condition.TaskType);
            if (condition.TaskManagement.HasValue) query = query.Where(u => u.TaskManagement == condition.TaskManagement);
            if (!string.IsNullOrEmpty(condition.TaskOverview)) query = query.Where(u => u.TaskOverview.Contains(condition.TaskOverview));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<TaskIssuedInfo>(query, condition.PageIndex, condition.PageSize);
        }
        #endregion
    }
}
