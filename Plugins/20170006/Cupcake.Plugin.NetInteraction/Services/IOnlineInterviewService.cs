using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineInterviewService
    {
        IPagedList<OnlineInterview> SearchAllOnlineInterview(OnlineInterviewCondition condition);
        void Add(OnlineInterview info);
        void Update(OnlineInterview info);

        OnlineInterview GetById(Guid Id);
        void Delete(OnlineInterview info); 
    }
}
