using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NLog;
using NLog.LayoutRenderers.Wrappers;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemCategoryDAOImpl : ItemCategoryDAO
    {
        private readonly IQueryable<ItemCategory> _itemCategories;
        private readonly ILogger _logger;

        public ItemCategoryDAOImpl(IDbProxyContext dbContext, ILogger logger)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (logger == null) throw new ArgumentNullException("logger");

            _itemCategories = dbContext.Set<ItemCategory>();
            _logger = logger;
        }

        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithChildren()
        {
            var query = 
                        from icp in _itemCategories
                        from icc in _itemCategories.DefaultIfEmpty() 
                        where icp.ParentItemCategoryId == null && icp.ItemCategoryId == icc.ParentItemCategoryId
                        group icp by new {icp.ItemCategoryId, icp.Name} into icpg
                        let item = icpg.Key
                        select new { item.Name, item.ItemCategoryId, Count = icpg.Count() };

            return query.ToList().Select(res => new ItemCategoryCount(res.Name, res.ItemCategoryId, res.Count));
        }

        /// <summary>
        /// Gets the top level categories with no children (sub categories).
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithNoChildren()
        {
            IQueryable<ItemCategoryCount> query = null;

            IEnumerable<ItemCategoryCount> topLevelCategoriesWithNoChildren = query.ToList();
            return topLevelCategoriesWithNoChildren;
        } 

        /// <summary>
        /// Gets the child categories (sub categories) by the parent id.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id)
        {
            IList<ItemCategoryCount> query = null;

            return query.ToList().Select(x => new ItemCategoryCount(x.Name, x.ItemCategoryId, x.TotalCount));
        } 
    }
}