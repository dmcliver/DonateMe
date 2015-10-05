using System;
using DonateMe.BusinessDomain.Entities;

namespace TestCommon.Builders
{
    public class ItemCategoryBuilder
    {
        private Guid _id;
        private string _name;

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
            ItemCategory parent = new ItemCategory(_id, _name);
            return parent;
        }

        public ItemCategoryBuilder With(string name)
        {
            _name = name;
            return this;
        }
    }
}