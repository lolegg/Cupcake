
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.NetWork.Data;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Plugin.NetWork.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetWork
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<WorkGuideService>().As<IWorkGuideService>().InstancePerLifetimeScope();
            builder.RegisterType<FormDownloadService>().As<IFormDownloadService>().InstancePerLifetimeScope();
            builder.RegisterType<ErrorReportingService>().As<IErrorReportingService>().InstancePerLifetimeScope();
            builder.RegisterType<OnlineAcceptanceService>().As<IOnlineAcceptanceService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "WorkGuide");
            this.RegisterPluginDataContext<PluginContext>(builder, "FormDownload");
            this.RegisterPluginDataContext<PluginContext>(builder, "ErrorReporting");
            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineAcceptance");
            //override required repository with our custom context
            builder.RegisterType<EfRepository<WorkGuide>>()
                .As<IRepository<WorkGuide>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("WorkGuide"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<FormDownload>>()
                .As<IRepository<FormDownload>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("FormDownload"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<ErrorReporting>>()
                .As<IRepository<ErrorReporting>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ErrorReporting"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<OnlineAcceptance>>()
                .As<IRepository<OnlineAcceptance>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineAcceptance"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
