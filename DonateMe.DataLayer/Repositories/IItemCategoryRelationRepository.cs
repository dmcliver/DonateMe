using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Common;

namespace DonateMe.DataLayer.Repositories
{
    [Injected]
    public interface IItemCategoryRelationRepository
    {
        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        IEnumerable<ItemCategory> GetTopLevelCategories();

        /// <summary>
        /// Gets the child categories by the parent id.
        /// </summary>
        IEnumerable<ItemCategory> GetChildCategoriesByParentId(Guid id);
    }
}