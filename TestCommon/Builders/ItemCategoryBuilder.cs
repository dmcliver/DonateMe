using System;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;

namespace TestCommon.Builders
{
    public class ItemCategoryBuilder
    {
        private Guid _id;
        private string _name;
        private ItemCategory _parent;

        public ItemCategoryBuilder(string name = "MyItemCategory")
        {
            _id = Guid.NewGuid();
            _name = name;
        }

        public ItemCategoryBuilder WithIdAndName(string id, string name)
        {
            _id = Guid.Parse(id);
            _name = name;
            return this;
        }

        public ItemCategory Build()
        {
            return _parent == null ? new ItemCategory(_id, _name) : new ItemCategory(_id, _name, _parent); 
        }

        public ItemCategoryCount BuildCount()
        {
            ItemCategoryCount count = new ItemCategoryCount(_name, _id, 0);
            return count;
        }

        public ItemCategoryBuilder With(string name)
        {
            _name = name;
            return this;
        }

        public ItemCategoryBuilder With(ItemCategory parent)
        {
            this._parent = parent;
            return this;
        }
    }
}