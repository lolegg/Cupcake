
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.Announcement.Data;
using Cupcake.Plugin.Announcement.Domain;
using Cupcake.Plugin.Announcement.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Announcement
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<AnnouncementService>().As<IAnnouncementService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "Announcement");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<AnnouncementInfo>>()
                .As<IRepository<AnnouncementInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Announcement"))
                .InstancePerLifetimeScope();
        }
        public int Order
        {
            get { return 1; }
        }
    }
}
