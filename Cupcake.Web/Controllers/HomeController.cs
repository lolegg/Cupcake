using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class HomeController : BasePublicController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //var a = base.CurrentUser;
            //string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };            

            //IEnumerable<string> query = names
            //    .Where(n => n.Contains("a"))
            //    .OrderBy(n => n.Length)
            //    .Select(n => n.ToUpper());

            // IEnumerable<string> query2 =
            //    from n in names
            //    where n.Contains("a")    
            //    orderby n.Length        
            //    select n.ToUpper();

            ////执行
            // foreach (string name in query)
            //     Console.WriteLine(name);

            return View();
        }
    }
}
