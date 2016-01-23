using System.Collections.Generic;
using System.Linq;
using DonateMe.Common;

namespace DonateMe.BusinessDomain
{
    [Injected]
    public interface CategoryHierarchyService
    {
        /// <summary>
        /// Sets the separator to use to separate the hierarchy of each grouped element for instance using the default " - " would display "GrandParent - Parent - Child"
        /// </summary>
        string Separator { set; }

        /// <summary>
        /// Groups the category names in a flattened hierarchy list, each element in the list displays its hierarchy by a separator.
        /// </summary>
        /// <param name="grouping">The grouping of parent and child categories.</param>
        IEnumerable<string> GroupCategoryNamesAsFlatListHierarchy(IEnumerable<IGrouping<string, ParentChildCategory>> grouping);
    }
}