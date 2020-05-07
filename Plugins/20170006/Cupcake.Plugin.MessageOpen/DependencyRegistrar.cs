
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Plugin.MessageOpen.Models;
using Cupcake.Plugin.MessageOpen.Services;
using Cupcake.Web.Framework;
using AutoMapper;


namespace Cupcake.Plugin.MessageOpen
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //builder.RegisterType<MessageOpenAdministrativeIndexService>().As<IMessageOpenAdministrativeIndexService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenAdministrativePunishService>().As<IMessageOpenAdministrativePunishService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenAnnualService>().As<IMessageOpenAnnualService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenMoneyMessageService>().As<IMessageOpenMoneyMessageService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenNormativeFileService>().As<IMessageOpenNormativeFileService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenPolicyService>().As<IMessageOpenPolicyService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenPolicyUnscrambleService>().As<IMessageOpenPolicyUnscrambleService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenRecentlyService>().As<IMessageOpenRecentlyService>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageOpenRightDutyListingService>().As<IMessageOpenRightDutyListingService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageOpenService>().As<IMessageOpenService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageOpenAnnouncementsService>().As<IMessageOpenAnnouncementsService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageOpenInstitutionalService>().As<IMessageOpenInstitutionalService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageOpenMechanismService>().As<IMessageOpenMechanismService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageOpenApplicationService>().As<IMessageOpenApplicationService>().InstancePerLifetimeScope();
            
            


            
            
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
           

            //override required repository with our custom context
        


            //builder.RegisterType<EfRepository<MessageOpenAdministrativeIndexInfo>>()
            //  .As<IRepository<MessageOpenAdministrativeIndexInfo>>()
            //  .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //  .InstancePerLifetimeScope();


            //builder.RegisterType<EfRepository<MessageOpenAdministrativePunishInfo>>()
            //  .As<IRepository<MessageOpenAdministrativePunishInfo>>()
            //  .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //  .InstancePerLifetimeScope();


            //builder.RegisterType<EfRepository<MessageOpenAnnualInfo>>()
            //  .As<IRepository<MessageOpenAnnualInfo>>()
            //  .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //  .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenMoneyMessageInfo>>()
            // .As<IRepository<MessageOpenMoneyMessageInfo>>()
            // .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            // .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenNormativeFileInfo>>()
            // .As<IRepository<MessageOpenNormativeFileInfo>>()
            // .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            // .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenPolicyInfo>>()
            // .As<IRepository<MessageOpenPolicyInfo>>()
            // .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            // .InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenPolicyUnscrambleInfo>>()
            //.As<IRepository<MessageOpenPolicyUnscrambleInfo>>()
            //.WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //.InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenRightDutyListingInfo>>()
            //.As<IRepository<MessageOpenRightDutyListingInfo>>()
            //.WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //.InstancePerLifetimeScope();

            //builder.RegisterType<EfRepository<MessageOpenRecentlyInfo>>()
            //.As<IRepository<MessageOpenRecentlyInfo>>()
            //.WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            //.InstancePerLifetimeScope();
            this.RegisterPluginDataContext<PluginContext>(builder, "MessageOpen");
            builder.RegisterType<EfRepository<MessageOpenInfo>>()
            .As<IRepository<MessageOpenInfo>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<MessageOpenAnnouncementsInfo>>()
              .As<IRepository<MessageOpenAnnouncementsInfo>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
              .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<MessageOpenInstitutionalInfo>>()
            .As<IRepository<MessageOpenInstitutionalInfo>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<MessageOpenMechanismInfo>>()
            .As<IRepository<MessageOpenMechanismInfo>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<MessageOpenApplicationInfo>>()
            .As<IRepository<MessageOpenApplicationInfo>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>("MessageOpen"))
            .InstancePerLifetimeScope();

            
            
        }
        public int Order
        {
            get { return 1; }
        }
    }
}
