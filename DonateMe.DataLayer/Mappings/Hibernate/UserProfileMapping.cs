using DonateMe.BusinessDomain.Entities;
using FluentNHibernate.Mapping;

namespace DonateMe.DataLayer.Mappings.Hibernate
{
    public class UserProfileMapping : ClassMap<UserProfile>
    {
        public UserProfileMapping()
        {
            Table("UserProfile");
            Id(u => u.UserId).GeneratedBy.Identity();
            Map(u => u.UserName).Not.Nullable();
        }
    }
}