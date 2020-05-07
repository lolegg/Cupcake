using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Jobs
{
    public class UARCJob : Job
    {
        public override void Execute()
        {
            //use for test
            using (StreamWriter writer =
             new StreamWriter(@"c:\temp\Cachecallback.txt", true))
            {
                writer.WriteLine("Cache Callback: {0}", DateTime.Now);
                writer.Close();
            }

        }
    }
}