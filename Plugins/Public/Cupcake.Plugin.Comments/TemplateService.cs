using Cupcake.Core.Plugins;
using Cupcake.Plugin.Comments.Data;
using System;
using Cupcake.Services;


namespace Cupcake.Plugin.Comments
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
            this.AddMenu("评论", "/Comments");
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
