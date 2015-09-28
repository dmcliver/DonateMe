using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetByCategoryId(Guid id);
    }
}