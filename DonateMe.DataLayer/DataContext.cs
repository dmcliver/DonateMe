using System.Data.Entity;
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
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
