
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.Template.Data;
using Cupcake.Plugin.Template.Domain;
using Cupcake.Plugin.Template.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Template
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "Template");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<News>>()
                .As<IRepository<News>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Template"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
