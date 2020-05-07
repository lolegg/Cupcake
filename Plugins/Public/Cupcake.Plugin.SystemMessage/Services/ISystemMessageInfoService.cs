using Cupcake.Core;
using Cupcake.Plugin.SystemMessage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.SystemMessage.Services
{
    public interface ISystemMessageInfoService
    {
        IPagedList<SystemMessageInfo> SearchAllSystemMessageInfo(SystemMessageInfoCondition condition);
       void Add(SystemMessageInfo info);
       void Update(SystemMessageInfo info);

       SystemMessageInfo GetById(Guid Id);
       void Delete(SystemMessageInfo info); 
    }
}
