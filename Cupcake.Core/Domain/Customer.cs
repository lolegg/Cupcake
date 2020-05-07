using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsVip { get;set;}
        public DateTime CreateDate { get;set;}
    }
}
