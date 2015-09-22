using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DonateMe.BusinessDomain.Entities;
using NLog;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemCategoryRelationRepository : IItemCategoryRelationRepository
    {
        private readonly ILogger _logger;
        private readonly DbSet<ItemCategoryRelation> _itemCategoryRelations;

        public ItemCategoryRelationRepository(IDataContext dataContext, ILogger logger)
        {
            if (dataContext == null) throw new ArgumentNullException("dataContext");
            if (logger == null) throw new ArgumentNullException("logger");

            _itemCategoryRelations = dataContext.Set<ItemCategoryRelation>();
            _logger = logger;
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
