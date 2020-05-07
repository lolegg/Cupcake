using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class DataDictionary : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }
        public string Tips { get; set; }
        public int? CustomType { get; set; }
        public string CustomAttributes { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class DataDictionaryCondition : Condition
    {
        public string Name { get; set; }
        public Guid NodeId { get; set; }
    }
}
