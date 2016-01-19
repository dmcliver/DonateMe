using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DonateMe.BusinessDomain;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    public class NavigatorController : ApiController
    {
        private readonly ItemCategoryDAO _itemCategoryDAO;

        /// <summary>
        /// Constructor for NavigatorController that has it's dependencies injected from IoC Container
        /// </summary>
        public NavigatorController(ItemCategoryDAO itemCategoryDAO)
        {
            if (itemCategoryDAO == null) throw new ArgumentNullException("itemCategoryDAO");
            _itemCategoryDAO = itemCategoryDAO;
        }

        /// <summary>
        ///  Handles an HTTP GET request for the corresponding url - http://localhost/Api/Navigator?id=5
        /// </summary>
        /// <param name="id">Category id to find items by</param>
        /// <returns>A list of found items that are to be returned as serialized json response</returns>
        public IEnumerable<ItemNodeModel> Get([FromUri] string id)
        {
            Guid gid;
            bool isTopLevel = !Guid.TryParse(id, out gid);

            if (isTopLevel)
            {
                IEnumerable<ItemCategoryCount> topLevelCategories = _itemCategoryDAO.GetTopLevelCategoriesWithChildren();
                IEnumerable<ItemNodeModel> itemNodeModels = topLevelCategories.Select(c => new ItemNodeModel(c.Name, c.ItemCategoryId, true));
                
                topLevelCategories = _itemCategoryDAO.GetTopLevelCategoriesWithNoChildren();
                return itemNodeModels.Concat(topLevelCategories.Select(c => new ItemNodeModel(c.Name, c.ItemCategoryId, false)));
            }

            IEnumerable<ItemCategoryCount> childCategories = _itemCategoryDAO.GetChildCategoriesByParentId(gid);
            return childCategories.Select(c => new ItemNodeModel(c, c.AnyChildren()));
        }
    }
}

