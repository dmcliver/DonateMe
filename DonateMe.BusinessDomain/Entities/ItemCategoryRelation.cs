using System;

namespace DonateMe.BusinessDomain.Entities
{
    public class ItemCategoryRelation
    {
        protected ItemCategoryRelation()
        {
        }

        public ItemCategoryRelation(ItemCategory parent, ItemCategory child)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (child == null) throw new ArgumentNullException("child");

            Parent = parent;
            ParentId = parent.ItemCategoryId;

            Child = child;
            ChildId = child.ItemCategoryId;
        }

        public Guid ParentId { get; private set; }

        public Guid ChildId { get; private set; }

        public ItemCategory Parent { get; private set; }
        public ItemCategory Child { get; private set; }
    }
}