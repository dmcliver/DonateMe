using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public static class ItemCategoryRelationMapping
    {
        public static void ConfigureCategoryRelation(DbModelBuilder modelBuilder)
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
    }
}