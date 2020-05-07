using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineCommentsService
    {
        IPagedList<OnlineComments> SearchAllOnlineComments(OnlineCommentsCondition condition);
        void Add(OnlineComments info);
        void Update(OnlineComments info);

        OnlineComments GetById(Guid Id);
        void Delete(OnlineComments info); 
    }
}
