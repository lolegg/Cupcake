using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Services;
using System.Linq;

namespace Cupcake.Plugin.MemberDengJi.Services
{
    public partial class GovernmentPurchasingService : PluginBaseService<Cupcake.Plugin.MemberDengJi.Domain.GovernmentPurchasing>, IGovernmentPurchasingService
    {
        public GovernmentPurchasingService(IRepository<Cupcake.Plugin.MemberDengJi.Domain.GovernmentPurchasing> repository)
            : base(repository)
        {
        }

        public IPagedList<GovernmentPurchasing> SearchGovernmentPurchasing(GovernmentPurchasingCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.PurchasingDepartment))
                query = query.Where(t => t.PurchasingDepartment.Contains(condition.PurchasingDepartment));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<GovernmentPurchasing>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
