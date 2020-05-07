using Cupcake.Plugin.NetWork.Domain;
using System;
namespace Cupcake.Plugin.NetWork.Services
{
    public interface IFormDownloadService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.NetWork.Domain.FormDownload> SearchFormDownload(Cupcake.Plugin.NetWork.Domain.FormDownloadCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.NetWork.Domain.FormDownload FormDownload);
        void InsertGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.FormDownload FormDownload);
        void UpdateGoogleProductRecord(Cupcake.Plugin.NetWork.Domain.FormDownload FormDownload);
        FormDownload GetById(Guid id);
    }
}
