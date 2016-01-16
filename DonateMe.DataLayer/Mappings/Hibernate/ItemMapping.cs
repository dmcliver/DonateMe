using DonateMe.BusinessDomain.Entities;
using FluentNHibernate.Mapping;

namespace DonateMe.DataLayer.Mappings.Hibernate
{
    public class ItemMapping : ClassMap<Item>
    {
        public ItemMapping()
        {
            Table("Item");
            Id(x => x.ItemId);
            References(i => i.Brand).Column("BrandId").ForeignKey("BrandId");
            References(i => i.ParentItemCategory).Column("ParentId").ForeignKey("ParentId").Not.Nullable();
            Map(i => i.Name).Not.Nullable();
            Map(i => i.Description);
            Map(i => i.Model);
        }
    }
}