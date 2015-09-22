using System;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            var itemCategory1 = new ItemCategory(Guid.NewGuid(), "Daddy");
            var itemCategory2 = new ItemCategory(Guid.NewGuid(), "Child");
            context.Set<ItemCategory>().Add(itemCategory1);
            context.Set<ItemCategory>().Add(itemCategory2);
            context.Set<ItemCategoryRelation>().Add(new ItemCategoryRelation(itemCategory1, itemCategory2));
            context.SaveChanges();
        }
    }
}

