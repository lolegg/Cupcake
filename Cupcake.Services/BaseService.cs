using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cupcake.Services
{
    public abstract class BaseService<T> where T : FrameworkBaseEntity
    {
        Repository<T> repository;
        public BaseService()
        {
            repository = new Repository<T>();
        }
        public virtual bool AlreadyExists(Expression<Func<T, bool>> where)
        {
            var rowCount = repository.NoTrackingDbSet.Count(where);
            if (rowCount == 0)
            {
                return false;
            }
            else
                return true;
        }
        public virtual int Add(T model)
        {
            repository.Add(model);
            return repository.Commit();
        }
        public virtual int Remove(T model)
        {
            repository.Remove(model);
            return repository.Commit();
        }
        public virtual int Modify(T model)
        {
            repository.Modify(model);
            return repository.Commit();
        }
        public virtual T Get(Guid id)
        {
            return repository.Get(id);
        }
        public virtual List<T> GetAll()
        {
            return repository.NoTrackingDbSet.Where(p => p.IsDelete == false).ToList();
        }
    }
}
