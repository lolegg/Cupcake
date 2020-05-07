
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.News.Data;
using Cupcake.Plugin.News.Domain;
using Cupcake.Plugin.News.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.News
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "News");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<NewsInfo>>()
                .As<IRepository<NewsInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("News"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
