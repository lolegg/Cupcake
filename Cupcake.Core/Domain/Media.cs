using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    public class Media : FrameworkBaseEntity
    {
        public string FileName { get; set; }        
        public string ExtensionName { get; set; }
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }
        public Guid MediaType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool IsPublic { get; set; }
    }

    public class MediaCondition : Condition
    {
         public string FileName { get; set; }
         public Guid? MediaType { get; set; }
    }
}
