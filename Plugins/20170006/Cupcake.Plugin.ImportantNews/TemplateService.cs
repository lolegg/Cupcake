using Cupcake.Core.Plugins;
using Cupcake.Plugin.ImportantNews.Data;
using Cupcake.Services;
namespace Cupcake.Plugin.ImportantNews
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
            this.AddMenu("要闻动态", "/ImportantNews");
            this.AddMenu("轮播图", "/Carousel");

            this.AddDataDictionary("新闻类型");
            this.AddDataDictionary("新闻类型", "本区新闻");
            this.AddDataDictionary("新闻类型", "各街道动态");
            this.AddDataDictionary("新闻类型", "国务院信息");
            this.AddDataDictionary("新闻类型", "社会组织动态");
            this.AddDataDictionary("新闻类型", "政务微博");

            this.AddDataDictionary("轮播图所属模块");
            this.AddDataDictionary("轮播图所属模块", "首页要闻动态");
            this.AddDataDictionary("轮播图所属模块", "要闻动态");
            this.AddDataDictionary("轮播图所属模块", "首页轮播");

            this.AddDataDictionary("路径所属模块");
            this.AddDataDictionary("路径所属模块", "本区新闻", "http://192.168.0.201:8021/ImportantNews/Details?newstype=1&Id=");
            this.AddDataDictionary("路径所属模块", "各街道动态", "http://192.168.0.201:8021/ImportantNews/Details?newstype=2&Id=");
            this.AddDataDictionary("路径所属模块", "国务院信息", "http://192.168.0.201:8021/ImportantNews/Details?newstype=3&Id=");
            this.AddDataDictionary("路径所属模块", "社会组织动态", "http://192.168.0.201:8021/ImportantNews/Details?newstype=4&Id=");
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
