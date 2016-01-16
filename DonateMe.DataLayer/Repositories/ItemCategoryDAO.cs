using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain;
using DonateMe.Common;
// ReSharper disable InconsistentNaming

namespace DonateMe.DataLayer.Repositories
{
    [Injected]
    public interface ItemCategoryDAO
    {
        /// <summary>
        /// Gets the top level categories with sub categories i.e. all the parents
        /// </summary>
        IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithChildren();

        /// <summary>
        /// Gets the child categories by the parent id.
        /// </summary>
        IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id);

        /// <summary>
        /// Gets the top level categories containing no subcategories.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithNoChildren();
    }
}