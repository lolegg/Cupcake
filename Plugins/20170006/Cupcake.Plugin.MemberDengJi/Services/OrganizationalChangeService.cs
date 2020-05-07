using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Services;
using System.Linq;

namespace Cupcake.Plugin.MemberDengJi.Services
{
    public partial class OrganizationalChangeService : PluginBaseService<Cupcake.Plugin.MemberDengJi.Domain.OrganizationalChange>, IOrganizationalChangeService
    {
        public OrganizationalChangeService(IRepository<Cupcake.Plugin.MemberDengJi.Domain.OrganizationalChange> repository): base(repository)
        {
        }

        public IPagedList<OrganizationalChange> SearchOrganizationalChange(OrganizationalChangeCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.UserName))
                query = query.Where(t => t.UserName.Contains(condition.UserName));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OrganizationalChange>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
