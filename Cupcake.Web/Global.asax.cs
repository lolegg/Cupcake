using Cupcake.Core.Domain;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Log4;
using Cupcake.Data;
using Cupcake.Services;
using Cupcake.Web.Controllers;
using Cupcake.Web.Jobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Cupcake.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static List<Job> JobQueue = new List<Job>();
        private static DateTime currentDate;
        protected void Application_Start()
        {
            string newdb = ConfigurationManager.AppSettings["CreateDatabaseIfNotExists"].ToLower();
            if (newdb == "true")
            {
                Database.SetInitializer<DbContextManager>(new InitializeDataForCreateDatabaseIfNotExists());
            }
            else
            {
                Database.SetInitializer<DbContextManager>(null);
            }

            //initialize engine context
            EngineContext.Initialize(false);
            //NopEngine ne = new NopEngine();
            //ne.Initialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterObjects();

            //RegisterCache("Heartbeat");
            currentDate = DateTime.Now.Date;
            //JobQueue.Add(new UARCJob() { Frequency = JobFrequency.EveryDay });
            //JobQueue.Add(new UARCUserJob { Frequency = JobFrequency.Immediately });

            //DbInterception.Add(new Log4NetInterceptor());
            //MiniProfilerEF6.Initialize();
            Log4Helper.Initialize(Server.MapPath("log4net.config"));

            //注册数据库操作拦截器类
            DbInterception.Add(new LogEFMonitor());
        }

        protected void Application_BeginRequest()
        {
            //MiniProfiler.Start();
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("heartbeat.html"))
            {
                RegisterCache("Heartbeat");
            }
        }

        protected void Application_EndRequest()
        {
            //MiniProfiler.Stop();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            //log error
            Cupcake.Core.Domain.User user = null;
            //TODO: 插件的错误Session会导致异常
            UserService userService = new UserService();
            try
            {
                if (Session["User"] == null)
                {
                    user = userService.GetSupperUser();
                }
                else
                {
                    user = Session["User"] as Cupcake.Core.Domain.User;
                }
            }
            catch (Exception ex)
            {
                user = userService.GetSupperUser();
                Log4Helper.Error(this.GetType(), ex, user.Id);
            }

            Log4Helper.Error(this.GetType(), lastError, user.Id);

            if (lastError != null)
            {
                Response.Clear();
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;

                IController errorController = new ErrorController();
                var routeData = new RouteData();

                if (lastError is HttpException)
                {
                    var httpException = lastError as HttpException;
                    if (httpException.GetHttpCode() == 404)
                    {
                        routeData.Values.Add("controller", "Error");
                        routeData.Values.Add("action", "PageNotFound");
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    }
                    else
                    {
                        routeData.Values.Add("controller", "Error");
                        routeData.Values.Add("action", "ErrorPage");
                        routeData.Values.Add("message", httpException.Message);
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                        //url will redirect, not good for seo
                        //Response.RedirectToRoute("Default", new { controller = "Error", action = "ErrorPage" });
                    }
                }
                else
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ServerError");
                    routeData.Values.Add("message", lastError.Message);
                    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                }
            }
        }

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            //var c = (new HttpRequestWrapper(Request)).IsAjaxRequest();
            //if (c)
            //{
            //    FormsAuthentication.SetAuthCookie("reloadsoft", true);
            //}
            //var a = HttpContext.Current.User;
        }

        private void RegisterCache(string key)
        {
            if (null == HttpContext.Current.Cache[key])
            {
                HttpContext.Current.Cache.Add(key, true, null,
                    DateTime.MaxValue, TimeSpan.FromMinutes(5),
                    CacheItemPriority.Normal,
                    new CacheItemRemovedCallback(CacheItemRemovedCallback));
            }
        }

        private void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {

            string DummyPageUrl = ConfigurationManager.AppSettings["HeartbeatUrl"].ToLower();
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);

            foreach (var job in JobQueue)
            {
                switch (job.Frequency)
                {
                    case JobFrequency.Immediately:
                        job.Execute();
                        break;
                    case JobFrequency.EveryHour:
                        job.Execute();
                        break;
                    case JobFrequency.EveryDay:
                        if (DateTime.Now.Date > currentDate)
                        {
                            currentDate = DateTime.Now.Date;
                            job.Execute();
                        }
                        break;
                    case JobFrequency.EveryWeek:
                        job.Execute();
                        break;
                    case JobFrequency.EveryMonth:
                        job.Execute();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
