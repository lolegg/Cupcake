using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.NetWork.Domain
{
    public class FormDownload : PluginBaseEntity
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
    }
    public class FormDownloadCondition : Condition
    {
        public string Title { get; set; }
        public Guid? WorkType { get; set; }
    }
}
