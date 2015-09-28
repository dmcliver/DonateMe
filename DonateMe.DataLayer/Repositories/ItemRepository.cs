﻿using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDataContext _dataContext;

        public ItemRepository(IDataContext dataContext)
        {
            if (dataContext == null) throw new ArgumentNullException("dataContext");
            _dataContext = dataContext;
        }

        public IEnumerable<Item> GetByCategoryId(Guid id)
        {
            return _dataContext.Set<Item>().Where(i => i.Category.ItemCategoryId == id).ToList();
        } 
    }
}