using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class Variables : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class VariablesCondition : Condition
    {
        public string Name { get; set; }
    }
}
