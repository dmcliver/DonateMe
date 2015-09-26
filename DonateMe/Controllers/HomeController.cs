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
            IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c));
            return View(itemNodeModels);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ActionResult GetChildren(object id)
        {
            IEnumerable<ItemCategory> childCategories = _itemCategoryRelationRepository.GetChildCategoriesByParentId((Guid)id);
            childCategories = childCategories as IList<ItemCategory> ?? childCategories.ToList();

            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationRepository.GetTopLevelCategories();

            if (childCategories.Any())
            {
                List<ItemNodeModel> itemNodeModels = BuildItemNodeModels(id, topLevelCategories, childCategories);
                return View("Index", itemNodeModels);
            }
            else
            {
                IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c)).ToList();
                //TODO: implement adding product gathering functionality here
                return View("Index", itemNodeModels);
            }
        }

        private static List<ItemNodeModel> BuildItemNodeModels(object id, IEnumerable<ItemCategory> topLevelCategories, IEnumerable<ItemCategory> childCategories)
        {
            List<ItemNodeModel> itemNodeModels =
            topLevelCategories.Select(
                c =>
                c.ItemCategoryId == (Guid) id ? 
                new ItemNodeModel(c, childCategories) :
                new ItemNodeModel(c)
            )
            .ToList();
            return itemNodeModels;
        }
    }
}
