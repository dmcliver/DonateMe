using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public static class ItemMapping
    {
        public static void ConfigureItem(DbModelBuilder modelBuilder)
        {
            var itemConfig = modelBuilder.Entity<Item>();

            itemConfig.ToTable("Item");

            itemConfig.HasRequired(i => i.ItemCategoryRelation)
                      .WithMany()
                      .HasForeignKey(i => new {i.ParentId, i.ChildId});

            itemConfig.HasOptional(i => i.Brand).WithMany().HasForeignKey(i => i.BrandId);

            itemConfig.Property(i => i.Description);
            itemConfig.Property(i => i.Model);
            itemConfig.Property(i => i.Name).IsRequired();

            itemConfig.HasKey(i => i.ItemId);
            itemConfig.Property(i => i.ItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}