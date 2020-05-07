using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Core.Domain
{
    public class SelectList<T>
    {
        public IPagedList<T> PageList { get; set; }
        public string SelectValueFiled { get; set; }
        public string SelectTextFiled { get; set; }
        public string HeadFiled { get; set; }
        public string HeadText { get; set; }
        public Paging Paging { get; set; }
    }
    public class SelectCondition : Condition
    {
        public string Name { get; set; }        
    }
}
