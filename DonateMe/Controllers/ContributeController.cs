using System.Web.Mvc;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    public class ContributeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ProductModel());
        }
    }
}