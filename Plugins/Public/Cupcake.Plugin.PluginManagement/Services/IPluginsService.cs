using System;
namespace Cupcake.Plugin.PluginManagement.Services
{
    public interface IPluginsService
    {
        bool AlreadyExists(string systemName, Guid id);
        Cupcake.Plugin.PluginManagement.Domain.Plugins GetPlugin(Guid id);
        void InsertPlugin(Cupcake.Plugin.PluginManagement.Domain.Plugins entity);
        Cupcake.Core.IPagedList<Cupcake.Plugin.PluginManagement.Domain.Plugins> SearchPlugins(Cupcake.Plugin.PluginManagement.Domain.PluginsCondition condition);
        void UpdatePlugin(Cupcake.Plugin.PluginManagement.Domain.Plugins entity);
    }
}
