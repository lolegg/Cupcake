
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using AutoMapper;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.MapMessage.Data;
using Cupcake.Plugin.MapMessage.Domain;
using Cupcake.Plugin.MapMessage.Models;
using Cupcake.Plugin.MapMessage.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MapMessage
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<MapMessageService>().As<IMapMessageService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);
            Mapper.CreateMap<MapMessageInfo, MapMessageModel>();
            Mapper.CreateMap<MapMessageModel, MapMessageInfo>();
            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "MapMessage");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<MapMessageInfo>>()
                .As<IRepository<MapMessageInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MapMessage"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
