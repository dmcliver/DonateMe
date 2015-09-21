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
    }
}