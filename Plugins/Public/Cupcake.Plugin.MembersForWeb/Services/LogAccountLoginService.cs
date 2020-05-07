using Cupcake.Data;
using Cupcake.Plugin.MembersForWeb.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.MembersForWeb.Services
{


     public partial class LogAccountLoginService : PluginBaseService<LogAccountLogin>, ILogAccountLoginService
    {
         public LogAccountLoginService(IRepository<LogAccountLogin> repository)
             : base(repository)
        {

        }
}

}
