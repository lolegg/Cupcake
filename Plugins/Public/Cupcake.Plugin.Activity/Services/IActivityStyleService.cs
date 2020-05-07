using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public interface IActivityStyleService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Activity.Domain.ActivityStyle> SearchActivityStyle(Cupcake.Plugin.Activity.Domain.ActivityStyleCondition condition);

        void DeleteGoogleProduct(Cupcake.Plugin.Activity.Domain.ActivityStyle ActivityStyle);
        void InsertGoogleProductRecord(Cupcake.Plugin.Activity.Domain.ActivityStyle ActivityStyle);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Activity.Domain.ActivityStyle ActivityStyle);
        ActivityStyle GetById(Guid id);
    }
}
