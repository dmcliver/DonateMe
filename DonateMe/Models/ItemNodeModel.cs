using System;
using DonateMe.BusinessDomain;

namespace DonateMe.Web.Models
{
    public class ItemNodeModel
    {
        public ItemNodeModel(ItemCategoryCount itemCategory, bool hasChildren = false):this(itemCategory.Name, itemCategory.ItemCategoryId, hasChildren)
        {
            if (itemCategory == null) throw new ArgumentNullException("itemCategory");
        }

        public ItemNodeModel(string name, Guid id, bool hasChildren)
        {
            text = name;
            this.id = id;
            children = hasChildren;
        }

        public string text { get; private set; }
        public Guid id { get; private set; }
        public bool children { get; private set; } 
    }
}
