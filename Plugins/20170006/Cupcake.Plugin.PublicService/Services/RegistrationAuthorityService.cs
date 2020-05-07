using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Services
{
    public class RegistrationAuthorityService : PluginBaseService<RegistrationAuthorityInfo>, IRegistrationAuthorityService
    {
        public RegistrationAuthorityService(IRepository<RegistrationAuthorityInfo> repository)
            : base(repository)
            {

            }
        public IPagedList<RegistrationAuthorityInfo> SearchRegistrationAuthority(RegistrationAuthorityCondition condition)
        {
            var query = repository.Table;

            //if (condition.BeginDate.HasValue&&condition.EndDate.HasValue)
            //{
            //    if (condition.BeginDate==condition.EndDate)
            //    {
            //        condition.EndDate = ((DateTime)condition.EndDate).AddDays(1);
            //    }
            //    query = query.Where(t => t.CreateDate >= condition.BeginDate);
            //    query = query.Where(t => t.CreateDate <= condition.EndDate);
            //}
            if (condition.ExecutiveStreet.HasValue) query = query.Where(t => t.ExecutiveStreet == condition.ExecutiveStreet);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<RegistrationAuthorityInfo>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
