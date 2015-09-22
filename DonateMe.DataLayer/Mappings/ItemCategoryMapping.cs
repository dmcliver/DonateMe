using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public static class ItemCategoryMapping
    {
        public static void ConfigureCategory(DbModelBuilder modelBuilder)
        {
            var itemCategoryConfg = modelBuilder.Entity<ItemCategory>();

            itemCategoryConfg.ToTable("ItemCategory");

            itemCategoryConfg.HasKey(ic => ic.ItemCategoryId)
                             .Property(ic => ic.Name)
                             .IsRequired();
        }
    }
}