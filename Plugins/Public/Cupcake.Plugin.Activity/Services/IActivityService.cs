using Cupcake.Plugin.Activity.Domain;
using System;
namespace Cupcake.Plugin.Activity.Services
{
    public interface IActivityService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Activity.Domain.Activitys> SearchActivity(Cupcake.Plugin.Activity.Domain.ActivityCondition condition);

        void DeleteGoogleProduct(Cupcake.Plugin.Activity.Domain.Activitys Activitys);
        void InsertGoogleProductRecord(Cupcake.Plugin.Activity.Domain.Activitys Activitys);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Activity.Domain.Activitys Activitys);
        Activitys GetById(Guid id);
    } 
}
