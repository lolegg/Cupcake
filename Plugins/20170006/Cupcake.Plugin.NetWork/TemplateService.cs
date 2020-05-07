using Cupcake.Core.Plugins;
using Cupcake.Plugin.NetWork.Data;
using Cupcake.Plugin.NetWork.Services;
using Cupcake.Services;

namespace Cupcake.Plugin.NetWork
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
            this.AddMenu("办事指南管理", "/WorkGuide/Index");
            this.AddMenu("表格下载管理", "/FormDownload/Index");
            this.AddMenu("表格下载错误", "/ErrorReporting/Index");
            this.AddMenu("在线受理", "/OnlineAcceptance/Index");

            this.AddDataDictionary("组织类型");
            this.AddDataDictionary("组织类型", "社会团体");
            this.AddDataDictionary("组织类型", "民办非企业单位");
            this.AddDataDictionary("组织类型", "基金会");
            this.AddDataDictionary("组织类型", "慈善组织");


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
