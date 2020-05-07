using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetWork.Data
{
    public partial class FormDownloadMap : PluginBaseEntityMapping<FormDownload>
    {
        public FormDownloadMap()
        {
            this.ToTable("NetWork_FormDownload");
        }
    }
}