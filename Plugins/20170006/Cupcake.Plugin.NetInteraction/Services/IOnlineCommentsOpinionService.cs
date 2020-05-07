using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineCommentsOpinionService
    {
        IPagedList<OnlineCommentsOpinion> SearchAllOnlineCommentsOpinion(OnlineCommentsOpinionCondition condition);
        void Add(OnlineCommentsOpinion info);
        void Update(OnlineCommentsOpinion info);

        OnlineCommentsOpinion GetById(Guid Id);
        void Delete(OnlineCommentsOpinion info); 
    }
}
