using Cupcake.Core.Plugins;
using Cupcake.Plugin.WorkNotice.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.WorkNotice
{
    public class TemplateService : BasePlugin
    {
        private readonly PluginContext efContext;
        public TemplateService(PluginContext efContext)
        {
            this.efContext = efContext;
        }

        public override void Install()
        {
            this.AddMenu("工作提醒", "/WorkNotice/Index");
            //this.AddMenuAtRoot("插件管理", "/PluginManagement");
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
