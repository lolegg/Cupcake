using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Core.Model
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
