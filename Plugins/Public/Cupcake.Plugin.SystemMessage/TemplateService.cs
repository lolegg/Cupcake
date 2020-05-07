using Cupcake.Core.Plugins;
using Cupcake.Plugin.SystemMessage.Data;
using System;
using Cupcake.Services;
namespace Cupcake.Plugin.SystemMessage
{
    public class TemplateService : BasePlugin
    {
        private readonly PluginContext efContext;
        public TemplateService(PluginContext efContext)
        {
            this.efContext = efContext;
        }

        //public override void Install()
        //{
        //    //this.AddMenuAtRoot("插件管理", "/PluginManagement");
        //    //data
        //    efContext.Install();            
        //    base.Install();
        //}

        //public override void Uninstall()
        //{
        //    //this.RemoveMenuAtRoot("插件管理", "/PluginManagement");
        //    //data
        //    efContext.Uninstall();
        //    base.Uninstall();
        //}


        public override void Install()
        {

            this.AddMenu("系统消息", "/SystemMessageInfo");
            //data
            efContext.Install();
            base.Install();
        }

        public override void Uninstall()
        {
         //   this.re("系统消息", "/SystemMessageInfo");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }


    }
}
