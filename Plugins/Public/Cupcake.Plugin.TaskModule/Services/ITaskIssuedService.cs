using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.TaskModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Services
{
    public interface ITaskIssuedService : IpluginBaseService<Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo>
    {
        //void DeleteGoogleProduct(Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo googleProductRecord);
        //System.Collections.Generic.IList<Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo> GetAll();
        //Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo GetById(object googleProductRecordId);
        //Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo GetByProductId(string productId);
        //void InsertGoogleProductRecord(Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo googleProductRecord);
        //void UpdateGoogleProductRecord(Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo googleProductRecord);
        //IList<TaskIssuedInfo> GetListByCondition(ref Paging paging, TaskIssuedCondition condition);
        Cupcake.Core.IPagedList<Cupcake.Plugin.TaskModule.Domain.TaskIssuedInfo> SearchTaskIssued(Cupcake.Plugin.TaskModule.Domain.TaskIssuedCondition condition);
    }
}
