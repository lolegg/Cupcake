using Cupcake.Data.Mappings;
using Cupcake.Core.Domain;
using System.Configuration;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public static class PluginBaseMappingExtensions
    {
        //无法读取到插件里的web.config
        //public static void ToPluginTable<TEntityType>(this PluginBaseMapping<TEntityType> plugin, string tableName) where TEntityType : BaseEntity
        //{
        //    string pluginName = ConfigurationManager.AppSettings["PluginName"];
        //    plugin.ToTable(pluginName + "_" + tableName);
        //}
        //public static void aa(this Controller controller)
        //{

        //}
    }
}
