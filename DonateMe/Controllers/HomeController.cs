using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Models;

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
            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren();
            //IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c));
            return View(new ItemCategoryModelContainer(new List<ItemNodeModel>()));
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ActionResult GetChildren(Guid id)
        {
            /*            IEnumerable<ItemCategory> childCategories = _itemCategoryRelationDAO.GetChildCategoriesByParentId(id);
            childCategories = childCategories as IList<ItemCategory> ?? childCategories.ToList();


            if (childCategories.Any())
            {
                return null;
            }*/

            {
                IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren();

                //IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c)).ToList();
                IEnumerable<Item> items = _itemDAO.GetByCategoryId(id);
                return View("Index", new ItemCategoryModelContainer());
            }
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
