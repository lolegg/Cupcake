﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Web.WapTemplate.Models
{
    public abstract class ListModel<T>
    {
        public IList<T> List { get; set; }
        public Paging Paging { get; set; }
    }
}
