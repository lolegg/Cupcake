using Cupcake.Core.Plugins;
using Cupcake.Plugin.Announcement.Data;
using System;
using Cupcake.Services;


namespace Cupcake.Plugin.Announcement
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
            this.AddMenu("通知公告", "/Announcement");
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
