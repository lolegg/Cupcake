using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MembersForWeb.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupcake.Plugin.MembersForWeb.Services
{
    public partial class MembersService : PluginBaseService<Members>, IMembersService
    {
        public MembersService(IRepository<Members> repository): base(repository)
        {

        }
        public IPagedList<Members> SearchNews(MembersCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.UserName))
                query = query.Where(t => t.UserName.Contains(condition.UserName));
            if (condition.UserType != null)
                query = query.Where(t => t.UserType == condition.UserType);
            if (condition.LastLoginDateBegin != null)
                query = query.Where(t => t.LastLoginDate >= condition.LastLoginDateBegin);
            if (condition.LastLoginDateEnd != null)
                query = query.Where(t => t.LastLoginDate <= condition.LastLoginDateEnd);
           

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Members>(query, condition.PageIndex, condition.PageSize);
        }
      


    }
}
