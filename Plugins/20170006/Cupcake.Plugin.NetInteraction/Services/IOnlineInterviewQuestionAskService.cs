using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineInterviewQuestionAskService
    {
        IPagedList<OnlineInterviewQuestionAsk> SearchAllOnlineInterviewQuestionAsk(OnlineInterviewQuestionAskCondition condition);
        void Add(OnlineInterviewQuestionAsk info);
        void Update(OnlineInterviewQuestionAsk info);

        OnlineInterviewQuestionAsk GetById(Guid Id);
        void Delete(OnlineInterviewQuestionAsk info); 
    }
}
