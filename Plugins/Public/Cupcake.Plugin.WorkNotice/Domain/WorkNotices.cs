using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.WorkNotice.Domain
{
    public class WorkNotices : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? WorkAbortDate { get; set; }
        public string Department { get; set; }
        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
    }
    public class WorkNoticeCondition : Condition
    {
        public string Title { get; set; }
    }
}
