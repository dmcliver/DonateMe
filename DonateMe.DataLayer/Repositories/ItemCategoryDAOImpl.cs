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
        private readonly IDbProxyContext _dbContext;
        private readonly IDbManager _manager;
        private readonly ILogger _logger;

        public ItemCategoryDAOImpl(IDbProxyContext dbContext, IDbManager manager, ILogger logger)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (manager == null) throw new ArgumentNullException("manager");
            if (logger == null) throw new ArgumentNullException("logger");

            _dbContext = dbContext;
            _manager = manager;
            _logger = logger;
        }

        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithChildren()
        {
            ISession session = _manager.ObtainSession();

            ItemCategory icp = null;

            IList<object[]> list = session.QueryOver<ItemCategory>()
                                          .JoinAlias(ic => ic.ParentItemCategory, () => icp)
                                          .Where(() => icp.ParentItemCategory == null)
                                          .SelectList
                                          (
                                              x => x.SelectGroup(() => icp.ItemCategoryId)
                                                    .SelectGroup(() => icp.Name)
                                                    .SelectCount(() => icp.ItemCategoryId)    
                                          )
                                          .List<object[]>();

            return list.Select(res => new ItemCategoryCount(res[1].ToString(), (Guid)res[0], (int)res[2]));
        }

        /// <summary>
        /// Gets the top level categories with no children (sub categories).
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetTopLevelCategoriesWithNoChildren()
        {
            ISession session = _manager.ObtainSession();

            ItemCategory icp = null;

            IList<object[]> list = session.QueryOver<ItemCategory>()
                                          .JoinAlias(ic => ic.ParentItemCategory, () => icp, JoinType.RightOuterJoin)
                                          .Where(ic => ic.ItemCategoryId == null && icp.ParentItemCategory == null)
                                          .SelectList
                                          (
                                              x => x.SelectGroup(() => icp.ItemCategoryId)
                                                    .SelectGroup(() => icp.Name)
                                                    .SelectCount(() => icp.ItemCategoryId)
                                          )
                                          .List<object[]>();

            return list.Select(res => new ItemCategoryCount(res[1].ToString(), (Guid)res[0], (int)res[2]));
        } 

        /// <summary>
        /// Gets the child categories (sub categories) by the parent id.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id)
        {
            IQueryable<ItemCategory> query = from ic in _dbContext.Set<ItemCategory>() 
                                             where ic.ParentItemCategoryId == id 
                                             select ic;

            IList<ItemCategory> itemCategories = query.ToList();
            
            return itemCategories.Select(ic => new ItemCategoryCount(ic.Name, ic.ItemCategoryId, itemCategories.Count));
        } 
    }
}
