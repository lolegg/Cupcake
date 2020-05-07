using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
        public abstract class Condition
        {
            private const int DefaultPageSize = 10;
            public int PageIndex { get; set; }

            private int pageSize;
            public int PageSize
            {
                get
                {
                    if (pageSize == 0)
                    {
                        return DefaultPageSize;
                    }
                    else
                        return pageSize;
                }
                set
                {
                    this.pageSize = value;
                }
            }
            public DateTime? BeginDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

}