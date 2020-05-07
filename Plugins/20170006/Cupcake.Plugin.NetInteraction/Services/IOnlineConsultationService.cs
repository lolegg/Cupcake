using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface IOnlineConsultationService
    {
        IPagedList<OnlineConsultation> SearchAllOnlineConsultation(OnlineConsultationCondition condition);
        void Add(OnlineConsultation info);
        void Update(OnlineConsultation info);

        OnlineConsultation GetById(Guid Id);
        void Delete(OnlineConsultation info);

        List<OnlineConsultation> GetserialNumber();
        
    }
}
