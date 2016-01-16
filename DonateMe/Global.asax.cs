using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DonateMe.DataLayer;
using DonateMe.Web.Filters;

namespace DonateMe.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            AutofacConfig.RegisterIoc();
            new InitializeSimpleMembershipAttribute().OnActionExecuting(null);
            RequestCompleted += MvcApplication_RequestCompleted;
        }

        void MvcApplication_RequestCompleted(object sender, System.EventArgs e)
        {
            DbManager.Close();
        }
    }
}