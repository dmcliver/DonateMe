using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemCategoryRelationRepository : IItemCategoryRelationRepository
    {
        private readonly DbSet<ItemCategoryRelation> _itemCategoryRelations;

        public ItemCategoryRelationRepository(IDataContext dataContext)
        {
            if (dataContext == null) throw new ArgumentNullException("dataContext");
            _itemCategoryRelations = dataContext.Set<ItemCategoryRelation>();
        }

        /// <summary>
        /// Gets the top level categories i.e. all the parents
        /// </summary>
        public IEnumerable<ItemCategory> GetTopLevelCategories()
        {
            IEnumerable<ItemCategory> query = from icrLeftIsParent in _itemCategoryRelations
                                              join icrRight in _itemCategoryRelations
                                              on icrLeftIsParent.ParentId equals icrRight.ChildId
                                              into rightEntity
                                              from rightsAndNulls in rightEntity.DefaultIfEmpty()
                                              where rightsAndNulls == null
                                              select icrLeftIsParent.Parent;

            return query.ToList();
        } 
    }
}
