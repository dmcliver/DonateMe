using System;

namespace DonateMe.BusinessDomain
{
    public class ItemCategoryCount
    {
        public string Name { get; private set; }
        public Guid ItemCategoryId { get; private set; }
        public long TotalCount { get; private set; }

        protected ItemCategoryCount()
        {
            Name = String.Empty;
            ItemCategoryId = Guid.Empty;
        }

        public ItemCategoryCount(string name, Guid itemCategoryId, long totalCount)
        {
            Name = name;
            ItemCategoryId = itemCategoryId;
            TotalCount = totalCount;
        }

        public bool AnyChildren()
        {
            return TotalCount > 0;
        }
    }
}