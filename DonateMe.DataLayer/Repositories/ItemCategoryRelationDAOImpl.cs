using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NLog;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemCategoryRelationDAOImpl : ItemCategoryRelationDAO
    {
        private readonly IQueryable<ItemCategoryRelation> _itemCategoryRelations;
        private readonly IQueryable<ItemCategory> _itemCategories;
        private readonly ILogger _logger;

        public ItemCategoryRelationDAOImpl(IDbProxyContext dbContext, ILogger logger)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (logger == null) throw new ArgumentNullException("logger");

            _itemCategoryRelations = dbContext.Set<ItemCategoryRelation>();
            _itemCategories = dbContext.Set<ItemCategory>();
            _logger = logger;
        }

        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        public IEnumerable<ItemCategory> GetTopLevelCategoriesWithChildren()
        {
            IQueryable<ItemCategory> query = (
                                                from icrLeftIsParent in _itemCategoryRelations
                                                join icrRight in _itemCategoryRelations
                                                on icrLeftIsParent.ParentId equals icrRight.ChildId
                                                into rightEntity
                                                from rightsAndNulls in rightEntity.DefaultIfEmpty()
                                                where rightsAndNulls == null
                                                select icrLeftIsParent.Parent
                                              )
                                              .Distinct();

            return query.ToList();
        }

        /// <summary>
        /// Gets the top level categories with no children (sub categories).
        /// </summary>
        public IEnumerable<ItemCategory> GetTopLevelCategoriesWithNoChildren()
        {
            IQueryable<ItemCategory> query = from ic in _itemCategories
                                             join icr in _itemCategoryRelations on ic.ItemCategoryId equals icr.Parent.ItemCategoryId
                                             into lefts
                                             from leftsWithNulls in lefts.DefaultIfEmpty()
                                             join icr2 in _itemCategoryRelations on ic.ItemCategoryId equals icr2.ChildId
                                             into lefts2
                                             from lefts2WithNulls in lefts2.DefaultIfEmpty()
                                             where leftsWithNulls == null && lefts2WithNulls == null
                                             select ic;

            List<ItemCategory> topLevelCategoriesWithNoChildren = query.ToList();
            return topLevelCategoriesWithNoChildren;
        } 

        /// <summary>
        /// Gets the child categories (sub categories) by the parent id.
        /// <returns>The Category containing a count the sub categories this sub category has</returns>
        /// </summary>
        public IEnumerable<ItemCategoryCount> GetChildCategoriesByParentId(Guid id)
        {
            var categoriesWithChildren = from children in _itemCategoryRelations
                                         join childCategoryIdCount in (
                                             from icr in _itemCategoryRelations
                                             join icrChild in _itemCategoryRelations on icr.Child.ItemCategoryId equals icrChild.Parent.ItemCategoryId
                                             into lefts
                                             from leftsWithNulls in lefts.DefaultIfEmpty()
                                             where icr.Parent.ItemCategoryId == id && leftsWithNulls != null
                                             group leftsWithNulls by leftsWithNulls.Parent.ItemCategoryId
                                             into gp
                                             select new {Id = gp.Key, TotalCount = gp.Count()}
                                         ) 
                                         on children.Parent.ItemCategoryId equals childCategoryIdCount.Id
                                         select new {Id = children.ParentId, childCategoryIdCount.TotalCount};

            var categoriesWithNoKids = from relation in _itemCategoryRelations
                                       join childRelation in _itemCategoryRelations on relation.Child.ItemCategoryId equals childRelation.Parent.ItemCategoryId
                                       into lefts
                                       from leftsWithNull in lefts.DefaultIfEmpty()
                                       where relation.Parent.ItemCategoryId == id && leftsWithNull == null
                                       select new {Id = relation.ChildId, TotalCount = 0};

            var query = from u in categoriesWithChildren.Union(categoriesWithNoKids)
                        join ic in _itemCategories on u.Id equals ic.ItemCategoryId
                        select new {ic.Name, ic.ItemCategoryId, u.TotalCount};

            return query.ToList().Select(x => new ItemCategoryCount(x.Name, x.ItemCategoryId, x.TotalCount));
        } 
    }
}