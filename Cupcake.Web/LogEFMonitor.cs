using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Web;

namespace Cupcake.Web
{
    public class LogEFMonitor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                //Logger.Error(command.CommandText, interceptionContext);
            }
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            string writesqlFlg = System.Configuration.ConfigurationManager.AppSettings["SqlWriteOrNotFlg"].ToUpper();
            if (writesqlFlg == "TRUE")
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into [Logs] values(newId(),'','Info','{0}','{1}','',getdate(),getdate(),0,null)", ((((System.Data.Entity.Infrastructure.Interception.DbInterceptionContext)(interceptionContext)).DbContexts as System.Collections.Generic.List<System.Data.Entity.DbContext>)).First().ToString(), command.CommandText);
                DBHelper.InsertData(sb.ToString(), DBHelper.connectionstring);
            }
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
           
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            string writesqlFlg = System.Configuration.ConfigurationManager.AppSettings["SqlWriteOrNotFlg"].ToUpper();
            if (writesqlFlg == "TRUE")
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into [Logs] values(newId(),'','Info','{0}','{1}','',getdate(),getdate(),0,null)", ((((System.Data.Entity.Infrastructure.Interception.DbInterceptionContext)(interceptionContext)).DbContexts as System.Collections.Generic.List<System.Data.Entity.DbContext>)).First().ToString(), command.CommandText);
                DBHelper.InsertData(sb.ToString(), DBHelper.connectionstring);
            }
        
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                //Logger.Error(command.CommandText, interceptionContext);
            }
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {

            string writesqlFlg = System.Configuration.ConfigurationManager.AppSettings["SqlWriteOrNotFlg"].ToUpper();
            if (writesqlFlg == "TRUE")
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into [Logs] values(newId(),'','Info','{0}','{1}','',getdate(),getdate(),0,null)", ((((System.Data.Entity.Infrastructure.Interception.DbInterceptionContext)(interceptionContext)).DbContexts as System.Collections.Generic.List<System.Data.Entity.DbContext>)).First().ToString(), command.CommandText);
                DBHelper.InsertData(sb.ToString(), DBHelper.connectionstring);
            }
        }
    }
}