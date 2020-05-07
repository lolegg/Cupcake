using Cupcake.Core.Plugins;
using Cupcake.Plugin.NetInteraction.Data;
using Cupcake.Services;
namespace Cupcake.Plugin.NetInteraction
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
            this.AddMenu("局长信箱", "/SecretaryMailbox");
            this.AddMenu("网上咨询", "/OnlineConsultation");
            this.AddMenu("在线访谈", "/OnlineInterview");
            this.AddMenu("网上征询", "/OnlineComments");
            this.AddMenu("网上投诉", "/OnlineComplaints");
            //data
            efContext.Install();            
            base.Install();
        }

        public override void Uninstall()
        {
           // this.d("插件管理", "/PluginManagement");
            //data
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
