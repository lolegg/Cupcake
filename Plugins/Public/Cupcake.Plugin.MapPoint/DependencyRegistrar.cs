
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using AutoMapper;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.MapPoint.Data;
using Cupcake.Plugin.MapPoint.Domain;
using Cupcake.Plugin.MapPoint.Models;
using Cupcake.Plugin.MapPoint.Services;
using Cupcake.Web.Framework;
using CPMD = Cupcake.Plugin.MapPoint.Domain;

namespace Cupcake.Plugin.MapPoint
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            Mapper.CreateMap<Map, MapModel>();
            Mapper.CreateMap<MapModel, Map>();

            Mapper.CreateMap<CPMD.MapPoint, MapPoint2Model>();
            Mapper.CreateMap<MapPoint2Model, CPMD.MapPoint>();

            Mapper.CreateMap<CPMD.MapPoint, MapCoordinateModel>();
            Mapper.CreateMap<MapCoordinateModel, CPMD.MapPoint>();

            builder.RegisterType<MapService>().As<IMapService>().InstancePerLifetimeScope();
            builder.RegisterType<Cupcake.Plugin.MapPoint.Services.MapPointService>().As<IMapPointService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "MapPoint");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Map>>()
                .As<IRepository<Map>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MapPoint"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<CPMD.MapPoint>>()
                .As<IRepository<CPMD.MapPoint>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MapPoint"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
