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

        [HttpPost]
        public ActionResult UploadProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            //TODO: Add call to repository to save item & redirect to success page

            return View("Index", new ProductModel{Name = "Success: " + model.Name});
        }
    }
}