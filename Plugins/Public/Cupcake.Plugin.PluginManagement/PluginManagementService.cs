using Cupcake.Core.Plugins;
using Cupcake.Plugin.PluginManagement.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.PluginManagement
{
    public class PluginManagementService : BasePlugin
    {
        private readonly PluginContext efContext;
        public PluginManagementService(PluginContext efContext)
        {
            this.efContext = efContext;
        }

        public override void Install()
        {
            this.AddDataDictionary("插件状态");
            this.AddDataDictionary("插件状态", "已注册");
            this.AddDataDictionary("插件状态", "开发中");
            this.AddDataDictionary("插件状态", "已发布");

            this.AddMenu("插件管理", "/PluginManagement");
            //data
            efContext.Install();            
            base.Install();
        }

        public override void Uninstall()
        {
            //this.RemoveMenuAtRoot("插件管理", "/PluginManagement");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
