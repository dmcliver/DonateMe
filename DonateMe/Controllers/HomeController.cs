using System;
using System.Web.Mvc;
using DonateMe.DataLayer.Repositories;

namespace DonateMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ItemCategoryRelationDAO _itemCategoryRelationDAO;
        private readonly ItemDAO _itemDAO;

        /// <summary>
        /// Constructor for HomeController that has it's dependencies injected from IoC Container
        /// </summary>
        public HomeController(ItemCategoryRelationDAO itemCategoryRelationDAO, ItemDAO itemDAO)
        {
            if (itemCategoryRelationDAO == null) throw new ArgumentNullException("itemCategoryRelationDAO");
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");

            _itemCategoryRelationDAO = itemCategoryRelationDAO;
            _itemDAO = itemDAO;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
