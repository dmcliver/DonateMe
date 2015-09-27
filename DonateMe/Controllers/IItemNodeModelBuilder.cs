using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer;
using DonateMe.Web.Models;

namespace DonateMe.Web.Controllers
{
    [Injected]
    public interface IItemNodeModelBuilder
    {
        List<ItemNodeModel> Build(Guid id, IEnumerable<ItemCategory> topLevelCategories, IEnumerable<ItemCategory> childCategories);
    }
}