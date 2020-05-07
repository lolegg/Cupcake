using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public interface ISelect
    {
        JsonResult SelectList(SelectCondition condition);
    }
}
