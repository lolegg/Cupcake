using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.PluginManagement.Domain;
using Cupcake.Services;
using System;
using System.Linq;

namespace Cupcake.Plugin.PluginManagement.Services
{
    public partial class PluginsService : PluginBaseService<Plugins>, IPluginsService
    {
        private readonly IRepository<Plugins> repository;
        public PluginsService(IRepository<Plugins> repository)
            : base(repository)
        {
            this.repository = repository;
        }

        public IPagedList<Plugins> SearchPlugins(PluginsCondition condition)
        {
            var query = repository.TableNoTracking;

            if (!string.IsNullOrEmpty(condition.SystemName))
                query = query.Where(t => t.SystemName.Contains(condition.SystemName));
            if (condition.PluginType != null)
                query = query.Where(t => t.PluginType == condition.PluginType);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Plugins>(query, condition.PageIndex, condition.PageSize);
        }

        public bool AlreadyExists(string systemName, Guid id)
        {
            var rowCount = repository.TableNoTracking.Count(p => p.SystemName == systemName && p.Id != id && p.IsDelete == false);
            if (rowCount == 0)
            {
                return false;
            }
            else
                return true;
        }
        public void InsertPlugin(Plugins entity)
        {
            repository.Insert(entity);
        }
        public Plugins GetPlugin(Guid id)
        {
            return repository.GetById(id);
        }
        public void UpdatePlugin(Plugins entity)
        {
            repository.Update(entity);
        }
    }
}
