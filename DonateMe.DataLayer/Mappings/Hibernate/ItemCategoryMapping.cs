using DonateMe.BusinessDomain.Entities;
using FluentNHibernate.Mapping;

namespace DonateMe.DataLayer.Mappings.Hibernate
{
    public class ItemCategoryMapping : ClassMap<ItemCategory>
    {
        public ItemCategoryMapping()
        {
            Table("ItemCategory");
            Id(i => i.ItemCategoryId);
            Map(i => i.Name).Column("Name").Not.Nullable();
            References(i => i.ParentItemCategory).Column("ParentItemCategoryId").ForeignKey("ParentItemCategoryId");
        }
    }
}