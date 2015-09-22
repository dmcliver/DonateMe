using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

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
         
            ConfigureCategory(modelBuilder);
            ConfigureCategoryRelation(modelBuilder);
            ConfigureItem(modelBuilder);
        }

        private void ConfigureItem(DbModelBuilder modelBuilder)
        {
            var itemConfig = modelBuilder.Entity<Item>();
            itemConfig.ToTable("Item");

            itemConfig.HasRequired(i => i.ItemCategoryRelation)
                      .WithMany()
                      .HasForeignKey(i => new {i.ParentId, i.ChildId});

            itemConfig.Property(i => i.Description);
            itemConfig.Property(i => i.Model);
            itemConfig.Property(i => i.Name).IsRequired();

            itemConfig.HasKey(i => i.ItemId);
            itemConfig.Property(i => i.ItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private static void ConfigureCategoryRelation(DbModelBuilder modelBuilder)
        {
            var itemCategoryRelationConfig = modelBuilder.Entity<ItemCategoryRelation>()
                                                         .HasKey(icr => new {icr.ParentId, icr.ChildId});

            itemCategoryRelationConfig.ToTable("ItemCategoryRelation");

            itemCategoryRelationConfig.HasRequired(icr => icr.Parent)
                                      .WithMany()
                                      .HasForeignKey(icr => icr.ParentId)
                                      .WillCascadeOnDelete(false);

            itemCategoryRelationConfig.HasRequired(icr => icr.Child)
                                      .WithMany()
                                      .HasForeignKey(icr => icr.ChildId)
                                      .WillCascadeOnDelete(false);
        }

        private static void ConfigureCategory(DbModelBuilder modelBuilder)
        {
            var itemCategoryConfg = modelBuilder.Entity<ItemCategory>();

            itemCategoryConfg.ToTable("ItemCategory");

            itemCategoryConfg.HasKey(ic => ic.ItemCategoryId)
                             .Property(ic => ic.Name)
                             .IsRequired();
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
