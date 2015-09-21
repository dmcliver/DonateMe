using System;

namespace DonateMe.BusinessDomain.Entities
{
    public class ItemCategory
    {
        protected ItemCategory()
        {
        }

        public ItemCategory(Guid itemCategoryId, string name)
        {
            if (itemCategoryId == null) 
                throw new ArgumentNullException("itemCategoryId");

            if (name == null) throw new ArgumentNullException("name");

            ItemCategoryId = itemCategoryId;
            Name = name;
        }

        public Guid ItemCategoryId { get; private set; }
        public string Name { get; private set; }
    }
}
