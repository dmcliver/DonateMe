using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Common;

namespace DonateMe.DataLayer.Repositories
{
    [Injected]
    public interface IItemRepository
    {
        IEnumerable<Item> GetByCategoryId(Guid id);
    }
}