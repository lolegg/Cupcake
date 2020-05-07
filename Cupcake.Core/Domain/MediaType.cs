using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    /// <summary>
    /// 媒体资源类型
    /// </summary>
    public class MediaType:BaseEntity
    {
        public MediaType() {
            this.Id = Guid.NewGuid();
        }
        public string Name { get; set; }

        public string EnName { get; set; }
    }
}
