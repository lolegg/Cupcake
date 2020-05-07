
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.WorkNotice.Data;
using Cupcake.Plugin.WorkNotice.Domain;
using Cupcake.Plugin.WorkNotice.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.WorkNotice
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<WorkNoticeService>().As<IWorkNoticeService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "WorkNotice");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<WorkNotices>>()
                .As<IRepository<WorkNotices>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("WorkNotice"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
