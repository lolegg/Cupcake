using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Comments.Services
{
    public interface IComments_CollectService
    {
        void DeleteGoogleProduct(Cupcake.Plugin.Comments.Domain.Comments_CollectInfo googleProductRecord);
        System.Collections.Generic.IList<Cupcake.Plugin.Comments.Domain.Comments_CollectInfo> GetAll();
        Cupcake.Plugin.Comments.Domain.Comments_CollectInfo GetById(Guid googleProductRecordId);
        void InsertGoogleProductRecord(Cupcake.Plugin.Comments.Domain.Comments_CollectInfo googleProductRecord);
        void UpdateGoogleProductRecord(Cupcake.Plugin.Comments.Domain.Comments_CollectInfo googleProductRecord);
    }
}
