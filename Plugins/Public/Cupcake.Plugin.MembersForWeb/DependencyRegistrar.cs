
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using AutoMapper;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.MembersForWeb.Data;
using Cupcake.Plugin.MembersForWeb.Domain;
using Cupcake.Plugin.MembersForWeb.Models;
using Cupcake.Plugin.MembersForWeb.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MembersForWeb
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<MembersService>().As<IMembersService>().InstancePerLifetimeScope();
            builder.RegisterType<LogAccountLoginService>().As<ILogAccountLoginService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);
            Mapper.CreateMap<Members, MembersModel>();
            Mapper.CreateMap<MembersModel, Members>();
            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "MembersForWeb");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Members>>()
                .As<IRepository<Members>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MembersForWeb"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LogAccountLogin>>()
                .As<IRepository<LogAccountLogin>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MembersForWeb"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
