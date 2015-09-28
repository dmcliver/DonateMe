using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.Web.Models
{
    public class ItemCategoryModelContainer
    {
        public ItemCategoryModelContainer(IEnumerable<ItemNodeModel> itemNodeModels, IEnumerable<Item> items)
        {
            Categories = itemNodeModels;
            Items = items;
        }

        public ItemCategoryModelContainer(IEnumerable<ItemNodeModel> itemNodeModels) :this(itemNodeModels, new List<Item>())
        {
        }

        public ItemCategoryModelContainer() : this(new List<ItemNodeModel>(), new List<Item>())
        {
        }

        public IEnumerable<ItemNodeModel> Categories { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}