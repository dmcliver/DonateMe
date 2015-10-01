using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    public class NavigatorController : ApiController
    {
        private readonly ItemCategoryRelationDAO _itemCategoryRelationDAO;
        private readonly ItemDAO _itemDAO;

                /// <summary>
        /// Constructor for NavigatorController that has it's dependencies injected from IoC Container
        /// </summary>
        public NavigatorController(ItemCategoryRelationDAO itemCategoryRelationDAO, ItemDAO itemDAO)
        {
            if (itemCategoryRelationDAO == null) throw new ArgumentNullException("itemCategoryRelationDAO");
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");

            _itemCategoryRelationDAO = itemCategoryRelationDAO;
            _itemDAO = itemDAO;
        }

        // GET api/navigator/5
        public IEnumerable<ItemNodeModel> Get(string id)
        {
            Guid gid;
            bool isTopLevel = !Guid.TryParse(id, out gid);

            if (isTopLevel)
            {
                IEnumerable<ItemCategory> topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren();
                IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c.Name, c.ItemCategoryId, true));
                
                topLevelCategories = _itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren();
                itemNodeModels = itemNodeModels.Concat(topLevelCategories.Select(c => new ItemNodeModel(c.Name, c.ItemCategoryId, false)));
                return itemNodeModels;
            }

            IEnumerable<ItemCategoryCount> childCategories = _itemCategoryRelationDAO.GetChildCategoriesByParentId(gid);
            return childCategories.Select(c => new ItemNodeModel(c, c.AnyChildren()));
        }
    }
}
