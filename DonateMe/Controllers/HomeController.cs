using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;

namespace DonateMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemCategoryRelationRepository _itemCategoryRelationRepository;

        public HomeController(IItemCategoryRelationRepository itemCategoryRelationRepository)
        {
            if (itemCategoryRelationRepository == null)
                throw new ArgumentNullException("itemCategoryRelationRepository");

            _itemCategoryRelationRepository = itemCategoryRelationRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationRepository.GetTopLevelCategories();
            return View(topLevelCategories);
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
