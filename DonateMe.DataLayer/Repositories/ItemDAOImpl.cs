using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemDAOImpl : ItemDAO
    {
        private readonly IDbProxyContext _dbContext;

        public ItemDAOImpl(IDbProxyContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        public IEnumerable<Item> GetByCategoryId(Guid id)
        {
            IQueryable<Item> query = _dbContext.Set<Item>().Where(i => i.ParentItemCategory.ItemCategoryId == id);
            return query.ToList();
        }
    }
}
