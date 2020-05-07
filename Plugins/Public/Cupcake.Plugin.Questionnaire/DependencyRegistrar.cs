
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.Questionnaire.Data;
using Cupcake.Plugin.Questionnaire.Domain;
using Cupcake.Plugin.Questionnaire.Services;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Questionnaire
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<QuestionnaireProblemService>().As<IQuestionnaireProblemService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireService>().As<IQuestionnaireService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionnaireAnswerService>().As<IQuestionnaireAnswerService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionStatisticService>().As<IQuestionStatisticService>().InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "Questionnaire");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<QuestionnaireInfo>>()
                .As<IRepository<QuestionnaireInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Questionnaire"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<QuestionnaireProblem>>()
               .As<IRepository<QuestionnaireProblem>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Questionnaire"))
               .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<QuestionnaireAnswer>>()
              .As<IRepository<QuestionnaireAnswer>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Questionnaire"))
              .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<QuestionStatistic>>()
             .As<IRepository<QuestionStatistic>>()
             .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Questionnaire"))
             .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
