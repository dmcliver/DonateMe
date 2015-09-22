using System;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer
{
    public class DbInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            var parentCategory = new ItemCategory(Guid.NewGuid(), "Daddy");
            context.Set<ItemCategory>().Add(parentCategory);

            var childCategory = new ItemCategory(Guid.NewGuid(), "Child");
            context.Set<ItemCategory>().Add(childCategory);

            var itemCategoryRelation = new ItemCategoryRelation(parentCategory, childCategory);
            context.Set<ItemCategoryRelation>().Add(itemCategoryRelation);

            Item i = new Item("Guitar", itemCategoryRelation);
            context.Set<Item>().Add(i);

            context.SaveChanges();
        }
    }
}

