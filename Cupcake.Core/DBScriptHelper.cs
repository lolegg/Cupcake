using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Core
{
    public class DBScriptHelper
    {
        public List<string> systemtablenamelist = null;
        public char splitstr = ';';
        public DBScriptHelper()
        {
            systemtablenamelist = new List<string>();
            systemtablenamelist.Add("table [dbo].[CustomPermissions]");
            systemtablenamelist.Add("table [dbo].[HasMenus]");
            systemtablenamelist.Add("table [dbo].[Menus]");
            systemtablenamelist.Add("table [dbo].[HasPermissions]");
            systemtablenamelist.Add("table [dbo].[Organizations]");
            systemtablenamelist.Add("table [dbo].[Roles]");
            systemtablenamelist.Add("table [dbo].[Users]");
            systemtablenamelist.Add("table [dbo].[Logs]");
            systemtablenamelist.Add("table [dbo].[MediaInfo]");
            systemtablenamelist.Add("table [dbo].[MediaTypes]");
            systemtablenamelist.Add("table [dbo].[UserRoles]");
            systemtablenamelist.Add("table [dbo].[Variables]");
        }
        public string GetSqlFilter(string dbscriptstr)
        {
            StringBuilder restr = new StringBuilder();
            string[] dbscriptarray = dbscriptstr.Split(splitstr);
            if (dbscriptstr.Length > 0)
            {
                foreach (string tempstr in dbscriptarray)
                {
                    bool issystem = false;
                    foreach (var systemtable in systemtablenamelist)
                    {
                        if (tempstr.Contains(systemtable))
                        {
                            issystem = true;
                            break;
                        }
                    }
                    if (!issystem)
                    {
                        restr.AppendLine(tempstr);
                    }
                }
            }
            return restr.ToString();
        }
    }
}
