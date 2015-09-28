using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    public class ItemNodeModelBuilder : IItemNodeModelBuilder
    {
        public List<ItemNodeModel> Build(Guid id, IEnumerable<ItemCategory> topLevelCategories, IEnumerable<ItemCategory> childCategories)
        {
            if (topLevelCategories == null) throw new ArgumentNullException("topLevelCategories");
            if (childCategories == null) throw new ArgumentNullException("childCategories");

            List<ItemNodeModel> itemNodeModels =
            topLevelCategories.Select
            (
                c =>
                c.ItemCategoryId == id ? 
                new ItemNodeModel(c, childCategories) :
                new ItemNodeModel(c)
            )
            .ToList();
            
            return itemNodeModels;
        }
    }
}