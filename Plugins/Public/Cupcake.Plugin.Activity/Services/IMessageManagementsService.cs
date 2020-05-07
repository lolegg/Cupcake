using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public interface IMessageManagementsService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Activity.Domain.MessageManagements> SearchMessageManagements(Cupcake.Plugin.Activity.Domain.MessageManagementsCondition condition,Guid id);

        void DeleteGoogleProduct(Cupcake.Plugin.Activity.Domain.MessageManagements MessageManagements);
        void InsertGoogleProductRecord(Cupcake.Plugin.Activity.Domain.MessageManagements MessageManagements);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Activity.Domain.MessageManagements MessageManagements);
        MessageManagements GetById(Guid id);
    }
}
