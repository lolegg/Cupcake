
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.SystemMessage.Data;
using Cupcake.Plugin.SystemMessage.Domain;
using Cupcake.Plugin.SystemMessage.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.SystemMessage
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<SystemMessageInfoService>().As<ISystemMessageInfoService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "SystemMessage");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<SystemMessageInfo>>()
                .As<IRepository<SystemMessageInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("SystemMessage"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
