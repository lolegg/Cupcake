using System;
using Autofac;
using Cupcake.Core.Infrastructure.DependencyManagement;
using Cupcake.Data;
using System.Configuration;

namespace Cupcake.Web.Framework
{
    /// <summary>
    /// Extensions for DependencyRegistrar
    /// </summary>
    public static class DependencyRegistrarExtensions
    {
        /// <summary>
        /// Register custom DataContext for a plugin
        /// </summary>
        /// <typeparam name="T">Class implementing IDbContext</typeparam>
        /// <param name="dependencyRegistrar">Dependency registrar</param>
        /// <param name="builder">Builder</param>
        /// <param name="contextName">Context name</param>
        public static void RegisterPluginDataContext<T>(this IDependencyRegistrar dependencyRegistrar,
            ContainerBuilder builder, string contextName)
             where T: IDbContext
        {
            //data layer
            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();

            //if (dataProviderSettings != null && dataProviderSettings.IsValid())
            //{
            //    //register named context
            //    builder.Register(c => (IDbContext)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
            //        .Named<IDbContext>(contextName)
            //        .InstancePerLifetimeScope();

            //    builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
            //        .InstancePerLifetimeScope();
            //}
            //else
            //{
                //register named context
            builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { ConfigurationManager.ConnectionStrings["DbContextManager"].ConnectionString }))
                .Named<IDbContext>(contextName)
                .InstancePerLifetimeScope();

            builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { ConfigurationManager.ConnectionStrings["DbContextManager"].ConnectionString }))
                .InstancePerLifetimeScope();
            //}
        }
    }
}
