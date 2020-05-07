using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace Cupcake.Data
{
    /// <summary>
    /// 泛型操作基类
    /// </summary>
    /// <typeparam name="TModel">对象类型</typeparam>
    public class Repository<TModel> : IDisposable  where TModel : class
    {
        private IDbSet<TModel> _entities;
        DbContextManager dbContext;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Repository()
        {
            dbContext = DbContextManager.Instance;
        }

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <param name="model">对象实例</param>
        public void Add(TModel model)
        {
            if (model != null)
            {
                GetSet().Add(model);
            }
        }
        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="model">对象实例</param>
        public void Remove(TModel model)
        {
            if (model != null)
            {
                GetSet().Remove(model);
            }
        }
        /// <summary>
        /// 修个一个对象
        /// </summary>
        /// <param name="model">对象实例</param>
        public void Modify(TModel model)
        {
            if (model != null)
            {
                dbContext.Entry<TModel>(model).State = EntityState.Modified;                
            }
        }
        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>对象实例</returns>
        public TModel Get(Guid id)
        {
            return GetSet().Find(id);
        }
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="filter">过滤表达式</param>
        /// <returns>对象实例</returns>
        public TModel Get(Expression<Func<TModel, bool>> filter)
        {
            return GetSet().FirstOrDefault(filter);
        }
        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用TrackQuery
        /// </summary>
        public IQueryable<TModel> NoTrackingDbSet
        {
            get { return GetSet().AsNoTracking(); }
        }
        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用NoTrackingQuery
        /// </summary>
        public IQueryable<TModel> TrackDbSet
        {
            get { return GetSet(); }
        }
        public int BatchUpdate(Expression<Func<TModel, bool>> filterExpression, Expression<Func<TModel, TModel>> updateExpression)
        {
            return GetSet().Where(filterExpression).Update(updateExpression);
        }
        public int BatchDelete(Expression<Func<TModel, bool>> filterExpression)
        {
            return GetSet().Delete(filterExpression);
        }
        public void UpdateSpecifiedAttribute(TModel model,string attributes)
        {
            GetSet().Attach(model);
            var stateEntry = ((IObjectContextAdapter)dbContext).ObjectContext.
                ObjectStateManager.GetObjectStateEntry(model);

            stateEntry.SetModifiedProperty(attributes);            
        }

        /// <summary>
        /// 把对象附加到上下文中，状态为未更改
        /// </summary>
        /// <param name="model">对象实例</param>
        /// <returns>返回该对象</returns>
        //public TModel Attach(TModel model)
        //{
        //    return GetSet().Attach(model);
        //}

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>数量</returns>
        //public int Count(Expression<Func<TModel, bool>> filter)
        //{
        //    return GetSet().Count(filter);
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="paging">分页信息</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="ascending">是否倒叙</param>
        /// <returns>IQueryable</returns>
        public IQueryable<TModel> GetPaged<KProperty>(ref Paging paging, Expression<Func<TModel, bool>> filter, Expression<Func<TModel, KProperty>> orderBy, bool ascending = false)
        {
            var set = this.NoTrackingDbSet;
            paging.Total = set.Count(filter);
            if (ascending)
            {
                return set.Where(filter).OrderBy(orderBy)
                          .Skip(paging.PageSize * paging.PageIndex)
                          .Take(paging.PageSize);
            }
            else
            {
                return set.Where(filter).OrderByDescending(orderBy)
                          .Skip(paging.PageSize * paging.PageIndex)
                          .Take(paging.PageSize);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="paging">分页信息</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>IQueryable</returns>
        public IQueryable<TModel> GetPaged<KProperty>(ref Paging paging, Expression<Func<TModel, bool>> filter)
        {
            var set = this.NoTrackingDbSet;
            paging.Total = set.Count(filter);

            return set.Where(filter)
                      .Skip(paging.PageSize * paging.PageIndex)
                      .Take(paging.PageSize);
        }

        /// <summary>
        /// 根据SQL查询分页数据（大数据快速分页），返回总条数
        /// </summary>
        /// <param name="sql">分页SQL语句</param>
        /// <param name="pars">参数数组</param>
        /// <returns></returns>
        public IEnumerable GetPaged(string sql, string orderBy, ref Paging paging, params object[] parameters)
        {
            var totalSql = string.Format("select count(1) from ({0}) total", sql);
            paging.Total = dbContext.Database.SqlQuery<int>(totalSql, parameters).Single();

            string sqlBuilder = @"select top " + paging.PageSize + " *   "
                                + "from "
                                + "("
                                    + "select row_number() over (order by " + orderBy + " ) as RowNumber,* from"
                                    + "("
                                    + sql
                                    + " ) T1 "
                                + ") T2 "
                                + "where RowNumber > " + paging.PageSize + "*(" + paging.PageIndex + ") ";

            return this.DynamicSqlQuery(sql, parameters.Select(x => ((ICloneable)x).Clone()).ToArray());
        }

        /// <summary>
        /// 释放上下文
        /// </summary>
        public void Dispose()
        {
            //if (efContext != null)
            //    DbContextManager.Instance.Dispose();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TModel> Table
        {
            get
            {
                return this.GetSet();
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TModel> TableNoTracking
        {
            get
            {
                return this.GetSet().AsNoTracking();
            }
        }
        /// <summary>
        /// 获取DbSet实例
        /// </summary>
        /// <returns>DbSet实例</returns>
        private IDbSet<TModel> GetSet()
        {
            if (_entities == null)
                _entities = dbContext.Set<TModel>();
            return _entities;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        /// <summary>
        /// 原始sql执行，必须有model对应，上下文会跟踪返回的实体
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>IEnumerable<TModel></returns>
        public IEnumerable<TModel> GetWithRawSql(string sql, params object[] parameters)
        {
            return dbContext.Set<TModel>().SqlQuery(sql, parameters);
        }
        /// <summary>
        /// 原始sql执行，TModel可以为基本类型，上下文不跟踪
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>IEnumerable<TModel></returns>
        public IEnumerable<TModel> GetwithdbSql(string sql, params object[] parameters)
        {
            return dbContext.Database.SqlQuery<TModel>(sql, parameters);
        }
        /// <summary>
        /// 执行sql，返回动态对象，属性自动对应sql语句查询出来的字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IEnumerable DynamicSqlQuery(string sql, params object[] parameters)
        {
            return dbContext.Database.DynamicSqlQuery(sql, parameters);
        }
        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params object[] parameters)
        {
            return dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
