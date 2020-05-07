using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Services;
using System;
using System.Linq;

namespace Cupcake.Plugin.PublicService.Services
{
    public partial class TipsForConvenienceService :PluginBaseService<TipsForConvenienceInfo>,ITipsForConvenienceService
    {
        public TipsForConvenienceService(IRepository<TipsForConvenienceInfo> repository)
            : base(repository)
            {

            }
        public IPagedList<TipsForConvenienceInfo> SearchTipsForConvenience(TipsForConvenienceCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.BeginDate.HasValue&&condition.EndDate.HasValue)
            {
                if (condition.BeginDate==condition.EndDate)
                {
                    condition.EndDate = ((DateTime)condition.EndDate).AddDays(1);
                }
                query = query.Where(t => t.CreateDate >= condition.BeginDate);
                query = query.Where(t => t.CreateDate <= condition.EndDate);
            }
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<TipsForConvenienceInfo>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
