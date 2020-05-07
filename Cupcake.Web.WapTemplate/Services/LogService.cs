using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class LogService
    {
        public int Add(LogAccountLogin info)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
              
                entity.LogAccountLogin.Add(info);
                var count = entity.SaveChanges();
                if (count > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

    }
}