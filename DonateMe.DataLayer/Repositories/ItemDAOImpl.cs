using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemDAOImpl : ItemDAO
    {
        private readonly IDataContext _dataContext;

        public ItemDAOImpl(IDataContext dataContext)
        {
            if (dataContext == null) throw new ArgumentNullException("dataContext");
            _dataContext = dataContext;
        }

        public IEnumerable<Item> GetByCategoryId(Guid id)
        {
            IQueryable<Item> query = _dataContext.Set<Item>().Where(i => i.ItemCategoryRelation.Child.ItemCategoryId == id);
            return query.ToList();
        }
    }
}
