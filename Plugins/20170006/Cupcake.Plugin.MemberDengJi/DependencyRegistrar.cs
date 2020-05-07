
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.MemberDengJi.Data;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Plugin.MemberDengJi.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MemberDengJi
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<OrganizationalChangeService>().As<IOrganizationalChangeService>().InstancePerLifetimeScope();
            builder.RegisterType<GovernmentPurchasingService>().As<IGovernmentPurchasingService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "OrganizationalChange");
            this.RegisterPluginDataContext<PluginContext>(builder, "GovernmentPurchasing");
            this.RegisterPluginDataContext<PluginContext>(builder, "MessageCenter");
            //override required repository with our custom context
            builder.RegisterType<EfRepository<OrganizationalChange>>()
                .As<IRepository<OrganizationalChange>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OrganizationalChange"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<GovernmentPurchasing>>()
                .As<IRepository<GovernmentPurchasing>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("GovernmentPurchasing"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<MessageCenter>>()
                .As<IRepository<MessageCenter>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageCenter"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
