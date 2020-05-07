using Cupcake.Plugin.NetWork.Domain;
using System;
namespace Cupcake.Plugin.NetWork.Services
{
    public interface IWorkGuideService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.NetWork.Domain.WorkGuide> SearchWorkGuide(Cupcake.Plugin.NetWork.Domain.WorkGuideCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.NetWork.Domain.WorkGuide WorkGuide);
        void InsertGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.WorkGuide WorkGuide);
        void UpdateGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.WorkGuide WorkGuide);
        WorkGuide GetById(Guid id);
    }
}
