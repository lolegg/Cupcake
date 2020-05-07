using Cupcake.Core.Plugins;
using Cupcake.Plugin.MessageOpen.Data;
using System;
using Cupcake.Services;


namespace Cupcake.Plugin.MessageOpen
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
            this.AddMenu("信息公开", "/MessageOpen");
            this.AddMenu("通知公告", "/MessageOpenAnnouncements");
            this.AddMenu("机构职能", "/MessageOpenInstitutional");
            this.AddMenu("工作机构", "/MessageOpenMechanism");
            this.AddMenu("申请信息", "/MessageOpenApplication");

            this.AddDataDictionary("信息公开类型");
            this.AddDataDictionary("信息公开类型", "行政处罚事项目录");
            this.AddDataDictionary("信息公开类型", "行政许可事项目录");
            this.AddDataDictionary("信息公开类型", "财政资金信息");
            this.AddDataDictionary("信息公开类型", "权利清单和责任清单");
            this.AddDataDictionary("信息公开类型", "信息公开年报");
            this.AddDataDictionary("信息公开类型", "规范性文件");
            this.AddDataDictionary("信息公开类型", "政策解读");
            this.AddDataDictionary("信息公开类型", "政策法规");
            this.AddDataDictionary("信息公开类型", "通知公告");
            this.AddDataDictionary("信息公开类型", "近期信息公开");

            this.AddDataDictionary("机构职能");
            this.AddDataDictionary("机构职能", "直属机构");
            this.AddDataDictionary("机构职能", "内设机构");
            this.AddDataDictionary("机构职能", "领导成员");
            this.AddDataDictionary("机构职能", "主要职责");

            this.AddDataDictionary("信息公开");
            this.AddDataDictionary("信息公开", "依申请公开");
            this.AddDataDictionary("信息公开", "政府信息公开指南");
            this.AddDataDictionary("信息公开", "政府信息公开目录");
            this.AddDataDictionary("信息公开", "政府信息公开规定");

            this.AddDataDictionary("是否置顶");
            this.AddDataDictionary("是否置顶","是");
            this.AddDataDictionary("是否置顶","否");

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
