using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Core
{
   public  interface IpluginBaseService<T> where T : PluginBaseEntity
    {
        void Add(T info);
        void Update(T info);
        T GetById(Guid Id);
        void Delete(T info);
        List<T> GetAll();
    }
}
