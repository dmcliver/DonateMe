using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using DonateMe.BusinessDomain;
using DonateMe.DataLayer.Mappings;

namespace DonateMe.DataLayer
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base("DonateMeDb")
        {
            Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ItemCategoryMapping.ConfigureCategory(modelBuilder);
            ItemCategoryRelationMapping.ConfigureCategoryRelation(modelBuilder);
            ItemMapping.ConfigureItem(modelBuilder);
            ImageMapping.ConfigureImage(modelBuilder);
            BrandMapping.ConfigureBrand(modelBuilder);
            UserProfileMappping.ConfigureProfile(modelBuilder);

            modelBuilder.ComplexType<UriType>();
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
