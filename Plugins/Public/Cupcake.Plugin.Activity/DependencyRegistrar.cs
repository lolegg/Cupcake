
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.Activity.Data;
using Cupcake.Plugin.Activity.Domain;
using Cupcake.Plugin.Activity.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Activity
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<ActivityService>().As<IActivityService>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationStatusService>().As<IRegistrationStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageManagementsService>().As<IMessageManagementsService>().InstancePerLifetimeScope();
            builder.RegisterType<ActivityStyleService>().As<IActivityStyleService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "Activity");
            this.RegisterPluginDataContext<PluginContext>(builder, "RegistrationStatus");
            this.RegisterPluginDataContext<PluginContext>(builder, "MessageManagements");
            this.RegisterPluginDataContext<PluginContext>(builder, "ActivityStyle");
            //override required repository with our custom context
            builder.RegisterType<EfRepository<Activitys>>()
                .As<IRepository<Activitys>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Activity"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<RegistrationStatus>>()
                .As<IRepository<RegistrationStatus>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("RegistrationStatus"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<MessageManagements>>()
                .As<IRepository<MessageManagements>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageManagements"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<ActivityStyle>>()
                .As<IRepository<ActivityStyle>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ActivityStyle"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
