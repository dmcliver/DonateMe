using System;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Tests.Builders
{
    public class ItemCategoryRelationBuilder
    {
        private ItemCategory _parent;
        private ItemCategory _child;

        public ItemCategoryRelationBuilder()
        {
            _parent = new ItemCategory(Guid.NewGuid(), "parent");
            _child = new ItemCategory(Guid.NewGuid(), "child");
        }

        public ItemCategoryRelationBuilder WithParentAndChild(Guid parentCategoryId, string parentCategoryName, Guid childCategoryId, string childCategoryName)
        {
            _parent = new ItemCategory(parentCategoryId, parentCategoryName);
            _child = new ItemCategory(childCategoryId, childCategoryName);
            return this;
        }

        public ItemCategoryRelationBuilder WithParentAndChild(string parentCategoryId, string parentCategoryName, string childCategoryId, string childCategoryName)
        {
            return WithParentAndChild(Guid.Parse(parentCategoryId), parentCategoryName, Guid.Parse(childCategoryId), childCategoryName);
        }

        public ItemCategoryRelationBuilder WithParentAndChild(ItemCategory parent, ItemCategory child)
        {
            _parent = parent;
            _child = child;
            return this;
        }

        public ItemCategoryRelation Build()
        {
            var relation = new ItemCategoryRelation(_parent, _child);
            return relation;
        }
    }
}