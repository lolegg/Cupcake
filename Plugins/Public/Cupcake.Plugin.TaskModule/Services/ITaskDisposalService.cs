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
    public interface ITaskDisposalService : IpluginBaseService<Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo>
    {
        //void DeleteGoogleProduct(Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo googleProductRecord);
        //System.Collections.Generic.IList<Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo> GetAll();
        //Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo GetById(object googleProductRecordId);
        Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo GetByTaskIssuedId(Guid taskIssuedId);
        //void InsertGoogleProductRecord(Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo googleProductRecord);
        //void UpdateGoogleProductRecord(Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo googleProductRecord);
        //IList<TaskDisposalInfo> GetListByCondition(ref Paging paging, TaskDisposalCondition condition);

        Cupcake.Core.IPagedList<Cupcake.Plugin.TaskModule.Domain.TaskDisposalInfo> SearchTaskDisposal(Cupcake.Plugin.TaskModule.Domain.TaskDisposalCondition condition);
    }
}
