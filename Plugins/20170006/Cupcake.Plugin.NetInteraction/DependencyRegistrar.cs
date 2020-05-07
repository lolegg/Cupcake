
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.NetInteraction.Data;
using Cupcake.Plugin.NetInteraction.Domain;
using Cupcake.Plugin.NetInteraction.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.NetInteraction
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<OnlineCommentsService>().As<IOnlineCommentsService>().InstancePerLifetimeScope();
            builder.RegisterType<OnlineComplaintsService>().As<IOnlineComplaintsService>().InstancePerLifetimeScope();

            builder.RegisterType<OnlineCommentsOpinionService>().As<IOnlineCommentsOpinionService>().InstancePerLifetimeScope();

            builder.RegisterType<OnlineConsultationService>().As<IOnlineConsultationService>().InstancePerLifetimeScope();

            builder.RegisterType<OnlineInterviewService>().As<IOnlineInterviewService>().InstancePerLifetimeScope();
            builder.RegisterType<OnlineInterviewQuestionAskService>().As<IOnlineInterviewQuestionAskService>().InstancePerLifetimeScope();
            builder.RegisterType<SecretaryMailboxService>().As<ISecretaryMailboxService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineComments");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineComments>>()
                .As<IRepository<OnlineComments>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineComments"))
                .InstancePerLifetimeScope();


            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineCommentsOpinion");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineCommentsOpinion>>()
                .As<IRepository<OnlineCommentsOpinion>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineCommentsOpinion"))
                .InstancePerLifetimeScope();



            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineComplaints");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineComplaints>>()
                .As<IRepository<OnlineComplaints>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineComplaints"))
                .InstancePerLifetimeScope();


            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineConsultation");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineConsultation>>()
                .As<IRepository<OnlineConsultation>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineConsultation"))
                .InstancePerLifetimeScope();


            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineInterview");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineInterview>>()
                .As<IRepository<OnlineInterview>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineInterview"))
                .InstancePerLifetimeScope();

            this.RegisterPluginDataContext<PluginContext>(builder, "OnlineInterviewQuestionAsk");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<OnlineInterviewQuestionAsk>>()
                .As<IRepository<OnlineInterviewQuestionAsk>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("OnlineInterviewQuestionAsk"))
                .InstancePerLifetimeScope();


            this.RegisterPluginDataContext<PluginContext>(builder, "SecretaryMailbox");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<SecretaryMailbox>>()
                .As<IRepository<SecretaryMailbox>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("SecretaryMailbox"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
