using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Comments.Services
{
    public interface IComments_PraiseService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo> SearchNotice(Cupcake.Plugin.Comments.Domain.Comments_PraiseCondition condition);
       void DeleteGoogleProduct(Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo googleProductRecord);
       System.Collections.Generic.IList<Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo> GetAll(); 
       Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo GetById(Guid googleProductRecordId);
       void InsertGoogleProductRecord(Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo googleProductRecord);
       void UpdateGoogleProductRecord(Cupcake.Plugin.Comments.Domain.Comments_PraiseInfo googleProductRecord);
    }
}
