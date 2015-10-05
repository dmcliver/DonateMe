using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DonateMe.DataLayer;
using NLog;

namespace DonateMe.Web
{
    public class AutofacConfig
    {
        public static void RegisterIoc()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // HttpContextBase injection
            builder.RegisterModule<AutofacWebTypesModule>();

            // View page injection
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly, typeof (DbContextImpl).Assembly)
                   .Where
                   (
                       t =>
                       t.GetInterfaces().Any(i => i.GetCustomAttributes().Any(a => a.GetType().Name == "InjectedAttribute"))
                   )
                   .AsImplementedInterfaces();

            builder.RegisterInstance(LogManager.GetCurrentClassLogger()).As<ILogger>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}