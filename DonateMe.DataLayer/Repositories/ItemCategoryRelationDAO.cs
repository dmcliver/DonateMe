using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Common;
// ReSharper disable InconsistentNaming

namespace DonateMe.DataLayer.Repositories
{
    [Injected]
    public interface ItemCategoryRelationDAO
    {
        /// <summary>
        /// Gets the top level categories with sub categories i.e. all the parents
        /// </summary>
        IEnumerable<ItemCategory> GetTopLevelCategoriesWithChildren();

        /// <summary>
        /// Gets the child categories by the parent id.
        /// </summary>
        IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id);

        /// <summary>
        /// Gets the top level categories containing no subcategories.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        IEnumerable<ItemCategory> GetTopLevelCategoriesWithNoChildren();
    }
}