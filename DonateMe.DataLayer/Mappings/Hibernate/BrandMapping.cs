using DonateMe.BusinessDomain.Entities;
using FluentNHibernate.Mapping;

namespace DonateMe.DataLayer.Mappings.Hibernate
{
    public class BrandMapping : ClassMap<Brand>
    {
        public BrandMapping()
        {
            Table("Brand");
            Id(b => b.BrandId).GeneratedBy.Identity();
            Map(b => b.Name).Unique().Not.Nullable();
        }
    }
}