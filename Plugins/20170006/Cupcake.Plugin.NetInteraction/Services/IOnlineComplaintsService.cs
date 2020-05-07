using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineComplaintsService
    {
        IPagedList<OnlineComplaints> SearchAllOnlineComplaints(OnlineComplaintsCondition condition);
        void Add(OnlineComplaints info);
        void Update(OnlineComplaints info);

        OnlineComplaints GetById(Guid Id);
        void Delete(OnlineComplaints info);
        List<OnlineComplaints> GetserialNumber();
        
    }
}
