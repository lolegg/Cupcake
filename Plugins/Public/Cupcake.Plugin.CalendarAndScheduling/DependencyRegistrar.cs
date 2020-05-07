
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.CalendarAndScheduling.Data;
using Cupcake.Plugin.CalendarAndScheduling.Domain;
using Cupcake.Plugin.CalendarAndScheduling.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.CalendarAndScheduling
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<CalendarAndSchedulingService>().As<ICalendarAndSchedulingService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "CalendarAndScheduling");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<CalendarAndSchedulingInfo>>()
                .As<IRepository<CalendarAndSchedulingInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("CalendarAndScheduling"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
