using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.Web.Models
{
    public class ItemNodeModel
    {
        public ItemNodeModel(ItemCategory itemCategory) : this(itemCategory, new List<ItemCategory>())
        {
        }

        public ItemNodeModel(ItemCategory itemCategory, IEnumerable<ItemCategory> childCategories)
        {
            Name = itemCategory.Name;
            Id = itemCategory.ItemCategoryId;

            List<ItemCategory> childItemCategories = childCategories.ToList();
            
            Children = childItemCategories.Any() ? 
                       childItemCategories.Select(c => new ItemNodeModel(c)).ToList() : 
                       new List<ItemNodeModel>();
        }

        public string Name { get; private set; }
        public object Id { get; private set; }
        public IEnumerable<ItemNodeModel> Children { get; private set; } 
    }
}
