using System;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Tests.Builders
{
    public class ItemCategoryBuilder
    {
        private Guid _id;
        private string _name;

        public ItemCategoryBuilder()
        {
            _id = Guid.NewGuid();
            _name = "MyItemCategory";
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
    }
}