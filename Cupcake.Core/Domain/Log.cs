using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    public class Log : FrameworkBaseEntity
    {
        public string Ip { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }

    public class LogCondition : Condition
    {
        public string Message { get; set; }
        public LogType? LogType { get; set; }
    }
}
