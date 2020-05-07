using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cupcake.Services
{
    public abstract class PluginBaseService<T> where T : PluginBaseEntity
    {
        public  readonly IRepository<T> repository;

        //public PluginBaseService()
        //{

        //}
        public PluginBaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }
        
        public virtual bool AlreadyExists(Expression<Func<T, bool>> where)
        {
            var rowCount = repository.TableNoTracking.Count(where);
            if (rowCount == 0)
            {
                return false;
            }
            else
                return true;
        }
        public virtual void Add(T model)
        {
            repository.Insert(model);
        }
        public virtual void Delete(T model)
        {
            repository.Delete(model);
        }
        public virtual void Update(T model)
        {
            repository.Update(model);
        }
        public virtual T GetById(Guid id)
        {
            return repository.GetById(id);
        }
        public virtual List<T> GetAll()
        {
            return repository.TableNoTracking.Where(p => p.IsDelete == false).ToList();
        }
    }
}
