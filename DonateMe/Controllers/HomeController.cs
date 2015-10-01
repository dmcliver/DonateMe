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
        private readonly IItemNodeModelBuilder _itemNodeModelBuilder;
        private readonly ItemDAO _itemDAO;

        /// <summary>
        /// Constructor for HomeController that has it's dependencies injected from IoC Container
        /// </summary>
        public HomeController(ItemCategoryRelationDAO itemCategoryRelationDAO, IItemNodeModelBuilder itemNodeModelBuilder, ItemDAO itemDAO)
        {
            if (itemCategoryRelationDAO == null) throw new ArgumentNullException("itemCategoryRelationDAO");
            if (itemNodeModelBuilder == null) throw new ArgumentNullException("itemNodeModelBuilder");
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");

            _itemCategoryRelationDAO = itemCategoryRelationDAO;
            _itemNodeModelBuilder = itemNodeModelBuilder;
            _itemDAO = itemDAO;
        }

        public ActionResult Index()
        {
            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategories();
            IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c));
            return View(new ItemCategoryModelContainer(itemNodeModels));
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ActionResult GetChildren(Guid id)
        {
            IEnumerable<ItemCategory> childCategories = _itemCategoryRelationDAO.GetChildCategoriesByParentId(id);
            childCategories = childCategories as IList<ItemCategory> ?? childCategories.ToList();

            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategories();

            if (childCategories.Any())
            {
                IList<ItemNodeModel> itemNodeModels = _itemNodeModelBuilder.Build(id, topLevelCategories, childCategories);
                return View("Index", new ItemCategoryModelContainer(itemNodeModels));
            }
            else
            {
                IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c)).ToList();
                IEnumerable<Item> items = _itemDAO.GetByCategoryId(id);
                return View("Index", new ItemCategoryModelContainer(itemNodeModels, items));
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
