
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cupcake.Core.Infrastructure;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using Cupcake.Plugin.Comments.Data;
using Cupcake.Plugin.Comments.Domain;
using Cupcake.Plugin.Comments.Models;
using Cupcake.Plugin.Comments.Services;
using Cupcake.Web.Framework;
using AutoMapper;


namespace Cupcake.Plugin.Comments
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            Mapper.CreateMap<CommentsInfo, CommentsModel>();
            Mapper.CreateMap<CommentsModel ,CommentsInfo>();
            builder.RegisterType<CommentsService>().As<ICommentsService>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(DependencyRegistrar).Assembly);

            //data context
            this.RegisterPluginDataContext<PluginContext>(builder, "Comments");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<CommentsInfo>>()
                .As<IRepository<CommentsInfo>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("Comments"))
                .InstancePerLifetimeScope();
        }
        public int Order
        {
            get { return 1; }
        }
    }
}
