using System.Web.Mvc;

namespace DonateMe.Web.Controllers
{
    /// <summary>
    /// Controller to redirect angularjs templateUrl paths to a template located in a cshtml partial view.
    /// </summary>
    public class DirectiveController : Controller
    {
        public ActionResult ProductDisplayTemplate()
        {
            return PartialView();
        }
    }
}
