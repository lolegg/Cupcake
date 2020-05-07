using Cupcake.Core.Plugins;
using Cupcake.Plugin.Activity.Data;
using Cupcake.Plugin.Activity.Services;
using Cupcake.Services;

namespace Cupcake.Plugin.Activity
{
    public class TemplateService : BasePlugin
    {
        private readonly PluginContext efContext;
        //
        public TemplateService(PluginContext efContext)
        {
            this.efContext = efContext;
        }

        public override void Install()
        {
            this.AddMenu("活动管理", "/Activity/Index");
            this.AddMenu("活动风采", "/ActivityStyle/Index");
            //data
            efContext.Install();            
            base.Install();
        }

        public override void Uninstall()
        {
            //this.RemoveMenuAtRoot("活动管理", "/Activity/Index");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
