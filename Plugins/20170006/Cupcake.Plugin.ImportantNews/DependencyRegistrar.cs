
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.ImportantNews.Data;
using Cupcake.Plugin.ImportantNews.Domain;
using Cupcake.Plugin.ImportantNews.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.ImportantNews
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<ImportantNewsService>().As<IImportantNewsService>().InstancePerLifetimeScope();
            builder.RegisterType<CarouselService>().As<ICarouselService>().InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "ImportantNews");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ImportantNewsInfo>>()
                .As<IRepository<ImportantNewsInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ImportantNews"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<CarouselInfo>>()
                 .As<IRepository<CarouselInfo>>()
                 .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ImportantNews"))
                 .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
