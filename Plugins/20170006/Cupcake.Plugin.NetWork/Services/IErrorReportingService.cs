using Cupcake.Plugin.NetWork.Domain;
using System;
namespace Cupcake.Plugin.NetWork.Services
{
    public interface IErrorReportingService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.NetWork.Domain.ErrorReporting> SearchErrorReporting(Cupcake.Plugin.NetWork.Domain.ErrorReportingCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.NetWork.Domain.ErrorReporting ErrorReporting);
        void InsertGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.ErrorReporting ErrorReporting);
        void UpdateGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.ErrorReporting ErrorReporting);
        ErrorReporting GetById(Guid id);
        string GetByType(Guid? Id);
    }
}
