using System;
using System.Collections.Generic;
using System.Web.Http;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;

namespace DonateMe.Web.Controllers
{
    public class ItemController : ApiController
    {
        private readonly ItemDAO _itemDAO;

        public ItemController(ItemDAO itemDAO)
        {
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");
            _itemDAO = itemDAO;
        }

        public IEnumerable<Item> Get(Guid id)
        {
            IEnumerable<Item> items = _itemDAO.GetByCategoryId(id);
            return items;
        }
    }
}
