using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class GeneralSettings : FrameworkBaseEntity
    {
        public string ProjectName { get; set; }
        public string Logo { get; set; }
        public string LoginTheme { get; set; }
    }
}
