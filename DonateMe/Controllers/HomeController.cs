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
        private readonly IItemNodeModelBuilder _itemNodeModelBuilder;

        public HomeController(IItemCategoryRelationRepository itemCategoryRelationRepository, IItemNodeModelBuilder itemNodeModelBuilder)
        {
            if (itemCategoryRelationRepository == null) throw new ArgumentNullException("itemCategoryRelationRepository");
            if (itemNodeModelBuilder == null) throw new ArgumentNullException("itemNodeModelBuilder");

            _itemCategoryRelationRepository = itemCategoryRelationRepository;
            _itemNodeModelBuilder = itemNodeModelBuilder;
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
        public ActionResult GetChildren(Guid id)
        {
            IEnumerable<ItemCategory> childCategories = _itemCategoryRelationRepository.GetChildCategoriesByParentId(id);
            childCategories = childCategories as IList<ItemCategory> ?? childCategories.ToList();

            IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationRepository.GetTopLevelCategories();

            if (childCategories.Any())
            {
                List<ItemNodeModel> itemNodeModels = _itemNodeModelBuilder.Build(id, topLevelCategories, childCategories);
                return View("Index", itemNodeModels);
            }
            else
            {
                IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c)).ToList();
                //TODO: implement adding product gathering functionality here
                return View("Index", itemNodeModels);
            }
        }
    }
}
