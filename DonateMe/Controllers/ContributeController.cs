using System.Web.Mvc;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    //[Authorize(Roles = "User")]
    public class ContributeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ProductModel());
        }
    }
}