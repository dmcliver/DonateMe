using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public static class UserProfileMappping
    {
        public static void ConfigureProfile(DbModelBuilder modelBuilder)
        {
            var userProfile = modelBuilder.Entity<UserProfile>();
            userProfile.ToTable("UserProfile");
            userProfile.HasKey(u => u.UserId);
            userProfile.Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            userProfile.Property(u => u.UserName).IsRequired();
        }
    }
}