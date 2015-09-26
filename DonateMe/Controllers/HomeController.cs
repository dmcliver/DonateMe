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
            if (id is Guid)
            {
                IEnumerable<ItemCategory> childCategories =_itemCategoryRelationRepository.GetChildCategoriesByParentId((Guid) id);
                childCategories = childCategories as IList<ItemCategory> ?? childCategories.ToList();
                if (childCategories.Any())
                {
                    IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationRepository.GetTopLevelCategories();

                    List<ItemNodeModel> itemNodeModels =
                    topLevelCategories.Select(
                        c =>
                        c.ItemCategoryId == (Guid) id ? 
                        new ItemNodeModel(c, childCategories) : 
                        new ItemNodeModel(c)
                    )
                    .ToList();
                    return View("Index", itemNodeModels);
                }
            }
            else
            {
                //TODO: implement gathering products to display
                return View("Index");
            }
            return View("Index");
        }
    }
}
