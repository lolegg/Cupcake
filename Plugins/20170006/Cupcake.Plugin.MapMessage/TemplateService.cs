using Cupcake.Core.Plugins;
using Cupcake.Services;
using Cupcake.Plugin.MapMessage.Data;

namespace Cupcake.Plugin.MapMessage
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
            this.AddMenu("协会列表", "/MapMessage");
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
