
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.PluginManagement.Data;
using Cupcake.Plugin.PluginManagement.Domain;
using Cupcake.Plugin.PluginManagement.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.PluginManagement
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<PluginsService>().As<IPluginsService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "PluginManagement");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Plugins>>()
                .As<IRepository<Plugins>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("PluginManagement"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
