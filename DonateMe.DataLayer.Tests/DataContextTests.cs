using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NUnit.Framework;

namespace DonateMe.DataLayer.Tests
{
    [TestFixture]
    public class DataContextTests
    {
        const string Path = "/Users/dmcliver";

        [Test]
        public void GeneratesItemWithImageOk()
        {
            DataContext context = new DataContext();

            BuildItemWithImage(context);

            context.SaveChanges();

            IList<Image> images = context.Set<Image>().ToList();

            Assert.True(images.Any());

            UriType uriPath = images.First().UriPath;
            Uri uri = uriPath.GetUri();
            Assert.That(uri.ToString(), Is.EqualTo(Path));
            Assert.That(uriPath.Path, Is.EqualTo(Path));
        }

        [Test]
        public void QueriesItemCategoryDataOk()
        {
            DataContext context = new DataContext();

            IList<ItemCategory> categories = context.Set<ItemCategory>().ToList();
            
            Assert.That(categories, Is.Not.Null);
        }

        private static void BuildItemWithImage(DataContext context)
        {
            var child = new ItemCategory(Guid.NewGuid(), "Instruments");
            var parent = new ItemCategory(Guid.NewGuid(), "Music");

            var itemCategoryRelation = new ItemCategoryRelation(parent, child);

            var item = new Item("Guitar", itemCategoryRelation);

            Image img = new Image(Guid.NewGuid(), new UriType(Path), item);

            AddEntities(context, child, parent, itemCategoryRelation, item, img);
        }

        private static void AddEntities(
            DataContext context, ItemCategory child, ItemCategory parent,
            ItemCategoryRelation itemCategoryRelation, Item item, Image img
        )
        {
            context.Set<ItemCategory>().Add(child);
            context.Set<ItemCategory>().Add(parent);
            context.Set<ItemCategoryRelation>().Add(itemCategoryRelation);
            context.Set<Item>().Add(item);
            context.Set<Image>().Add(img);
        }
    }
}
