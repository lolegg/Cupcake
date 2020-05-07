using Cupcake.Core.Plugins;
using Cupcake.Services;
using Cupcake.Plugin.MembersForWeb.Data;

namespace Cupcake.Plugin.MembersForWeb
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
            //this.AddMenuAtRoot("插件管理", "/PluginManagement");
            this.AddMenu("用户", "/MembersForWeb");
            //data
            efContext.Install();            
            base.Install();
        }

        public override void Uninstall()
        {
            //this.RemoveMenuAtRoot("插件管理", "/PluginManagement");
            //this.AddMenu("用户", "/CalendarAndScheduling");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
