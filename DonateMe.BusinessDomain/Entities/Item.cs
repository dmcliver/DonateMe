using System;

namespace DonateMe.BusinessDomain.Entities
{
    public class Item
    {
        public Item(string name)
        {
            Name = name;
        }

        protected Item() {}

        public int ItemId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public string Model { get; set; }

        public Guid ParentId { get; private set; }
        public Guid ChildId { get; private set; }

        public ItemCategoryRelation ItemCategoryRelation { get; private set; }
    }
}
