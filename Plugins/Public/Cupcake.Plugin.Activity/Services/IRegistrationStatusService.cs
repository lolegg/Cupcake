using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public interface IRegistrationStatusService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Activity.Domain.RegistrationStatus> SearchRegistrationStatus(Cupcake.Plugin.Activity.Domain.RegistrationStatusCondition condition);

        void DeleteGoogleProduct(Cupcake.Plugin.Activity.Domain.RegistrationStatus RegistrationStatus);
        void InsertGoogleProductRecord(Cupcake.Plugin.Activity.Domain.RegistrationStatus RegistrationStatus);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Activity.Domain.RegistrationStatus RegistrationStatus);
    }
}
