
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using AutoMapper;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.PublicService.Data;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Plugin.PublicService.Models;
using Cupcake.Plugin.PublicService.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.PublicService
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<TipsForConvenienceService>().As<ITipsForConvenienceService>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationAuthorityService>().As<IRegistrationAuthorityService>().InstancePerLifetimeScope();
            builder.RegisterType<SocialOrganizationNameService>().As<ISocialOrganizationNameService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);
            Mapper.CreateMap<TipsForConvenienceInfo, TipsForConvenienceModel>();
            Mapper.CreateMap<TipsForConvenienceModel, TipsForConvenienceInfo>();

            Mapper.CreateMap<RegistrationAuthorityInfo, RegistrationAuthorityModel>();
            Mapper.CreateMap<RegistrationAuthorityModel, RegistrationAuthorityInfo>();

            Mapper.CreateMap<SocialOrganizationNameInfo, SocialOrganizationNameModel>();
            Mapper.CreateMap<SocialOrganizationNameModel, SocialOrganizationNameInfo>();
            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "PublicService");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<TipsForConvenienceInfo>>()
                .As<IRepository<TipsForConvenienceInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("PublicService"))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<RegistrationAuthorityInfo>>()
               .As<IRepository<RegistrationAuthorityInfo>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("PublicService"))
               .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<SocialOrganizationNameInfo>>()
               .As<IRepository<SocialOrganizationNameInfo>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("PublicService"))
               .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
