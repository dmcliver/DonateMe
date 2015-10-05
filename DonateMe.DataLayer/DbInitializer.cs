using System;
using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer
{
    public class DbInitializer : DropCreateDatabaseAlways<DbContextImpl>
    {
        protected override void Seed(DbContextImpl context)
        {
            base.Seed(context);

            ItemCategoryRelation musicCategoryRelation = BuildCategoryRelations(context);

            Item i = new Item("Guitar", musicCategoryRelation);
            context.Set<Item>().Add(i);

            context.SaveChanges();
        }

        private static ItemCategoryRelation BuildCategoryRelations(DbContextImpl context)
        {
            var parentCategory = new ItemCategory(Guid.Parse("A3C7A21C-4C61-4A7A-B7D3-3B7D6EFA6F8A"), "Music");
            context.Set<ItemCategory>().Add(parentCategory);

            var childCategory = new ItemCategory(Guid.Parse("057ED8FF-84D9-4263-8722-E2A963924B4B"), "Instruments");
            context.Set<ItemCategory>().Add(childCategory);

            var musicCategoryRelation = new ItemCategoryRelation(parentCategory, childCategory);
            context.Set<ItemCategoryRelation>().Add(musicCategoryRelation);

            var videoCategory = new ItemCategory(Guid.Parse("00F5CA2C-8717-4DBA-8A1A-8A687457B2A6"), "Video");
            context.Set<ItemCategory>().Add(videoCategory);

            var instrumentCategoryRelation = new ItemCategoryRelation(parentCategory, videoCategory);
            context.Set<ItemCategoryRelation>().Add(instrumentCategoryRelation);

            var bookCategory = new ItemCategory(Guid.Parse("BF273661-F572-4A6F-974B-5DB56F9DADBA"), "Books");
            context.Set<ItemCategory>().Add(bookCategory);

            var stringedMusicCategory = new ItemCategory(Guid.Parse("F9E6DBA6-4593-4484-B5CE-9F17266C2D5D"), "Stringed");
            context.Set<ItemCategory>().Add(stringedMusicCategory);

            var stringedCategoryRelation = new ItemCategoryRelation(childCategory, stringedMusicCategory);
            context.Set<ItemCategoryRelation>().Add(stringedCategoryRelation);
            return musicCategoryRelation;
        }
    }
}

