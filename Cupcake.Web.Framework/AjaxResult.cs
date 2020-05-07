using Cupcake.Core.Domain;

namespace Cupcake.Web.Framework
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            Message = "";
        }
        public Result Result { get; set; }
        public string Message { get; set; }
    }
    public class TreeAjaxResult : AjaxResult
    {
        public string CurrentNodeId { get; set; }
    }
    public class UploadAjaxResult : AjaxResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ExtensionName { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Path { get; set; }
        public string RelativePath { get; set; }
        public string ThumbnailPath { get; set; }

    }
}
