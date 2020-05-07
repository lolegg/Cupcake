using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Web.Jobs
{
    public abstract class Job
    {
        public JobFrequency Frequency { get; set; }
        public abstract void Execute();
    }
}
