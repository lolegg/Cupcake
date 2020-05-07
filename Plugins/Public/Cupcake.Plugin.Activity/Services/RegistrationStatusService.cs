using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public partial class RegistrationStatusService:IRegistrationStatusService
    {
        private readonly IRepository<RegistrationStatus> repository;
        public RegistrationStatusService(IRepository<RegistrationStatus> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(RegistrationStatus RegistrationStatus)
        {
            if (RegistrationStatus == null)
                throw new ArgumentNullException("RegistrationStatus");

            repository.Delete(RegistrationStatus);
        }

        public virtual void InsertGoogleProductRecord(RegistrationStatus RegistrationStatus)
        {
            if (RegistrationStatus == null)
                throw new ArgumentNullException("RegistrationStatus");

            repository.Insert(RegistrationStatus);
        }

        public virtual void UpdateGoogleProductRecord(RegistrationStatus RegistrationStatus)
        {
            if (RegistrationStatus == null)
                throw new ArgumentNullException("RegistrationStatus");

            repository.Update(RegistrationStatus);
        }

        public IPagedList<RegistrationStatus> SearchRegistrationStatus(RegistrationStatusCondition condition)
        {
            var query = repository.Table;
            if(condition.SubordinateActivitiesID!=null)
            {
                query = query.Where(t => t.SubordinateActivitiesID == condition.SubordinateActivitiesID);
            }
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<RegistrationStatus>(query, condition.PageIndex, condition.PageSize);
        }


    }
}
