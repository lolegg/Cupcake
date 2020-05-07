using Cupcake.Core.Plugins;
using Cupcake.Plugin.MemberDengJi.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.MemberDengJi
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
            this.AddMenu("组织变更信息登记", "/OrganizationalChange/Index");
            this.AddMenu("政府购买服务登记", "/GovernmentPurchasing/Index");
            efContext.Install();                  
            base.Install();
        }

        public override void Uninstall()
        {
            efContext.Uninstall();
            base.Uninstall();
        }
    }
}
