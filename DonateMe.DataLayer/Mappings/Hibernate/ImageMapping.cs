using DonateMe.BusinessDomain.Entities;
using FluentNHibernate.Mapping;

namespace DonateMe.DataLayer.Mappings.Hibernate
{
    public class ImageMapping : ClassMap<Image>
    {
        public ImageMapping()
        {
            Table("Image");
            Id(i => i.ImageId);
            Map(i => i.Path).Column("FilePath").Not.Nullable();
            References(i => i.Item).Column("ItemId").ForeignKey("ItemId").Not.Nullable();
        }
    }
}