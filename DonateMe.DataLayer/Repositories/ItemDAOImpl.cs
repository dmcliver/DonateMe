﻿using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
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

        public IEnumerable<IGrouping<string, ParentChildCategory>> GetCategories()
        {
            return _dbContext.Set<ItemCategory>()
                             .Select(ic => new ParentChildCategory(ic.Name, ic.ParentItemCategory))
                             .GroupBy(pcc => pcc.Parent)
                             .ToList();
        }

        public int Save(Item model)
        {
            _dbContext.Add(model);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<string> GetCategoryNames()
        {
            return _dbContext.Set<ItemCategory>().Select(ic => ic.Name).ToList();
        }
    }
}
