using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NHibernate;
using NHibernate.SqlCommand;
using NLog;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemCategoryDAOImpl : ItemCategoryDAO
    {
        private readonly IQueryable<ItemCategory> _itemCategories;
        private readonly IDbManager _manager;
        private readonly ILogger _logger;

        public ItemCategoryDAOImpl(IDbProxyContext dbContext, IDbManager manager, ILogger logger)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (manager == null) throw new ArgumentNullException("manager");
            if (logger == null) throw new ArgumentNullException("logger");

            _itemCategories = dbContext.Set<ItemCategory>();
            _manager = manager;
            _logger = logger;
        }

        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithChildren()
        {
//            var query = from icp in _itemCategories
//                        from icc in _itemCategories
//                        where icp.ParentItemCategoryId == null && icp.ItemCategoryId == icc.ParentItemCategoryId
//                        group icp by new { icp.ItemCategoryId, icp.Name } into icpg
//                        let item = icpg.Key
//                        select new { item.Name, item.ItemCategoryId, Count = icpg.Count() };
//
//            return query.ToList().Select(res => new ItemCategoryCount(res.Name, res.ItemCategoryId, res.Count));
            
            ISession session = _manager.ObtainSession();

            ItemCategory icp = null;

            IList<Object[]> list = session.QueryOver<ItemCategory>()
                                          .JoinAlias(ic => ic.ParentItemCategory, () => icp, JoinType.InnerJoin)
                                          .Where(() => icp.ParentItemCategory == null)
                                          .SelectList
                                          (
                                              x => x.SelectGroup(() => icp.ItemCategoryId)
                                                    .SelectGroup(() => icp.Name)
                                                    .SelectCount(() => icp.ItemCategoryId)    
                                          )
                                          .List<Object[]>();

            return list.Select(res => new ItemCategoryCount(res[1].ToString(), (Guid)res[0], (int)res[2]));
        }

        /// <summary>
        /// Gets the top level categories with no children (sub categories).
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithNoChildren()
        {
//            var query = from icp in _itemCategories
//                        join icc in _itemCategories on icp.ItemCategoryId equals icc.ParentItemCategoryId
//                        into iccg from leftIcc in iccg.DefaultIfEmpty()
//                        where icp.ParentItemCategoryId == null && leftIcc == null
//                        group icp by new { icp.ItemCategoryId, icp.Name } into icpg
//                        let item = icpg.Key
//                        select new { item.Name, item.ItemCategoryId, Count = icpg.Count() };
//
//            return query.ToList().Select(res => new ItemCategoryCount(res.Name, res.ItemCategoryId, res.Count));

            ISession session = _manager.ObtainSession();

            ItemCategory icp = null;

            IList<Object[]> list = session.QueryOver<ItemCategory>()
                                          .JoinAlias(ic => ic.ParentItemCategory, () => icp, JoinType.RightOuterJoin)
                                          .Where(() => icp.ParentItemCategory == null)
                                          .Where(ic => ic.ItemCategoryId == null)
                                          .SelectList
                                          (
                                              x => x.SelectGroup(() => icp.ItemCategoryId)
                                                    .SelectGroup(() => icp.Name)
                                                    .SelectCount(() => icp.ItemCategoryId)
                                          )
                                          .List<Object[]>();

            return list.Select(res => new ItemCategoryCount(res[1].ToString(), (Guid)res[0], (int)res[2]));
        } 

        /// <summary>
        /// Gets the child categories (sub categories) by the parent id.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id)
        {
            IQueryable<ItemCategory> query = from ic in _itemCategories 
                                             where ic.ParentItemCategoryId == id 
                                             select ic;

            IList<ItemCategory> itemCategories = query.ToList();
            
            return itemCategories.Select(x => new ItemCategoryCount(x.Name, x.ItemCategoryId, itemCategories.Count));
        } 
    }
}
