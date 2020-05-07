using Cupcake.Core.Plugins;
using Cupcake.Services;
using Cupcake.Plugin.CalendarAndScheduling.Data;

namespace Cupcake.Plugin.CalendarAndScheduling
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
          this.AddMenu("工作排班", "/CalendarAndScheduling");
            //data
            efContext.Install();            
            base.Install();
        }

        public override void Uninstall()
        {
            // this.("工作排班", "/CalendarAndScheduling");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
