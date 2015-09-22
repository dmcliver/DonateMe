using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
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

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // HttpContextBase injection
            builder.RegisterModule<AutofacWebTypesModule>();

            // View page injection
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly, typeof (DataContext).Assembly)
                   .Where
                   (
                       t =>
                       t.GetInterfaces().Any(i => i.GetCustomAttributes().Any(a => a.GetType().Name == "InjectedAttribute"))
                   )
                   .AsImplementedInterfaces();

            builder.RegisterInstance(LogManager.GetCurrentClassLogger()).As<ILogger>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}