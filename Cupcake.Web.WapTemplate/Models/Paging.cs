using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class Paging
    {
        public Paging(Condition condition)
        { 
            PageIndex = condition.PageIndex;
            PageSize = condition.PageSize;
        }
        public Paging()
        {
        }

        private int pageIndex;
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                pageIndex = value;
            }
        }
        private int pageSize;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        public int Total { get; set; }
    }
    }
