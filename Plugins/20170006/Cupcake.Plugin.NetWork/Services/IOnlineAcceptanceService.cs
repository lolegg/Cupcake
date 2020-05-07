using Cupcake.Plugin.NetWork.Domain;
using System;
namespace Cupcake.Plugin.NetWork.Services
{
    public interface IOnlineAcceptanceService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.NetWork.Domain.OnlineAcceptance> SearchOnlineAcceptance(Cupcake.Plugin.NetWork.Domain.OnlineAcceptanceCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.NetWork.Domain.OnlineAcceptance OnlineAcceptance);
        void InsertGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.OnlineAcceptance OnlineAcceptance);
        void UpdateGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.OnlineAcceptance OnlineAcceptance);
        OnlineAcceptance GetById(Guid id);
    }
}
