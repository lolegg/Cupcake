using Autofac;
using Autofac.Core;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Core.Infrastructure;
using Cupcake.Plugin.TaskModule.Services;
using Autofac.Integration.Mvc;
using Cupcake.Data;
using Cupcake.Plugin.TaskModule.Domain;
using Cupcake.Plugin.TaskModule.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.TaskModule
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<TaskIssuedService>().As<ITaskIssuedService>().InstancePerLifetimeScope();
            builder.RegisterType<TaskDisposalService>().As<ITaskDisposalService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "TaskModule");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<TaskIssuedInfo>>()
                .As<IRepository<TaskIssuedInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("TaskModule"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<TaskDisposalInfo>>()
              .As<IRepository<TaskDisposalInfo>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>("TaskModule"))
              .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
