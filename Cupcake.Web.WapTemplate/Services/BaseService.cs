using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class BaseService<TModel> where TModel : class
    {
      
        /// <summary>
        /// add 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Add(TModel model)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            { 
               entity.Set<TModel>().Add(model);
              return entity.SaveChanges();
            }
           
        }
        /// <summary>
        /// 添加并返回实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TModel AddReturn(TModel model)
        {

            using (CupcakeEntities entity = new CupcakeEntities())
            {
                entity.Set<TModel>().Add(model);
                entity.SaveChanges();
                return model;
            }
          
        }
        /// <summary>
        /// 删除实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Remove(TModel model)
        {

            using (CupcakeEntities entity = new CupcakeEntities())
            {
                entity.Set<TModel>().Remove(model);
               return  entity.SaveChanges();
                
            }
           
        }
        //public virtual int Modify(TModel model)
        //{
        //    using (CupcakeEntities entity = new CupcakeEntities())
        //    {
               
        //        entity.Set<TModel>().(model);
        //        return entity.SaveChanges();

        //    }
         
        //}
       /// <summary>
       /// 获取所有
       /// </summary>
       /// <returns></returns>
        public virtual List<TModel> GetAll()
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Set<TModel>().AsNoTracking().ToList();
            }
        }
        /// <summary>
        /// 根据过滤条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual List<TModel> GetList(Expression<Func<TModel, bool>> filter)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Set<TModel>().AsNoTracking().Where(filter).ToList();
            }
        }

        /// <summary>
        /// 根据过滤条件获取单个
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual TModel Get(Expression<Func<TModel, bool>> filter)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Set<TModel>().Where(filter).SingleOrDefault();
            }
        }

    }
}