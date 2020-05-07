using Cupcake.Core.Plugins;
using Cupcake.Plugin.MapPoint.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.MapPoint
{
    public class MapPointService : BasePlugin
    {
        private readonly PluginContext efContext;
        public MapPointService(PluginContext efContext)
        {
            this.efContext = efContext;
        }

        public override void Install()
        {
            this.AddMenu("地图标点", "/MapPoint");
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
