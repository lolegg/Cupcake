using Cupcake.Core.Plugins;
using Cupcake.Plugin.PublicService.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.PublicService
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
            this.AddMenu("便民提示", "/TipsForConvenience");
            this.AddMenu("登记管理机关", "/RegistrationAuthority");
            this.AddMenu("社会组织孵化基地", "/SocialOrganizationName");
            this.AddDataDictionary("登记管理机关类别");
            this.AddDataDictionary("登记管理机关类别", "区登记管理机关");
            this.AddDataDictionary("登记管理机关类别", "街道登记管理机关");
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
