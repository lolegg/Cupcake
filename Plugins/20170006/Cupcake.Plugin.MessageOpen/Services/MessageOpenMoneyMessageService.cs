﻿using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Linq;
using Cupcake.Services;
using Cupcake.Plugin.MessageOpen.Helper;
using System;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public partial class MessageOpenMoneyMessageService : PluginBaseService<MessageOpenMoneyMessageInfo>, IMessageOpenMoneyMessageService
    {
        public MessageOpenMoneyMessageService(IRepository<MessageOpenMoneyMessageInfo> repository)
            : base(repository)
        {
         
        }
       public IPagedList<MessageOpenMoneyMessageInfo> SearchNews(MessageOpenMoneyMessageCondition condition)
         {
             var query = repository.Table;

             if (!string.IsNullOrEmpty(condition.Title))
                 query = query.Where(t => t.Title.Contains(condition.Title));
             if (condition.Title != null)
                 query = query.Where(t => t.Title == condition.Title);
             if (condition.IsTop != null)
             {
                 bool IsTop = bool.Parse(DataDictionaryHelper.GetValue((Guid)condition.IsTop));
                 query = query.Where(t => t.IsTop == IsTop);
             }
             if (condition.Department != null)
                 query = query.Where(t => t.Department.Contains(condition.Department));
             query = query.Where(t => t.IsDelete == false);
             query = query.OrderByDescending(t => t.IsTop).ThenByDescending(t => t.CreateDate);

             return new PagedList<MessageOpenMoneyMessageInfo>(query, condition.PageIndex, condition.PageSize);
         }
    }
}
