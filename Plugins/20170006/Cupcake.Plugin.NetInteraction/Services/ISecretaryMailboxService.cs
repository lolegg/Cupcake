using Cupcake.Core;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public interface ISecretaryMailboxService
    {
        IPagedList<SecretaryMailbox> SearchAllSecretaryMailbox(SecretaryMailboxCondition condition);
        void Add(SecretaryMailbox info);
        void Update(SecretaryMailbox info);

        SecretaryMailbox GetById(Guid Id);
        void Delete(SecretaryMailbox info);


        List<SecretaryMailbox> GetserialNumber();
    }
}
