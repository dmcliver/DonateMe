using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Common;
// ReSharper disable InconsistentNaming

namespace DonateMe.DataLayer.Repositories
{
    [Injected]
    public interface ItemDAO
    {
        IEnumerable<Item> GetByCategoryId(Guid id);
        int Save(Item model);
        IEnumerable<IGrouping<string, ParentChildCategory>> GetCategories();
    }
}