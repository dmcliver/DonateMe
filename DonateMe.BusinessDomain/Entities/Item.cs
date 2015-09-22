using System;
using System.Collections.Generic;

namespace DonateMe.BusinessDomain.Entities
{
    public class Item
    {
        private Brand _brand;

        public Item(string name, ItemCategoryRelation itemCategoryRelation)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (itemCategoryRelation == null) throw new ArgumentNullException("itemCategoryRelation");

            Name = name;
            ItemCategoryRelation = itemCategoryRelation;

            ChildId = itemCategoryRelation.ChildId;
            ParentId = itemCategoryRelation.ParentId;
        }

        protected Item() {}

        public int ItemId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public string Model { get; set; }

        public Guid ParentId { get; private set; }
        public Guid ChildId { get; private set; }
        public int? BrandId { get; private set; }

        public ItemCategoryRelation ItemCategoryRelation { get; private set; }

        public Brand Brand
        {
            get { return _brand; }
            set
            {
                if (value != null)
                {
                    BrandId = value.BrandId;
                    _brand = value;
                }
            }
        }

        public ItemCategory Category
        {
            get { return ItemCategoryRelation.Child; }
        }
    }
}
