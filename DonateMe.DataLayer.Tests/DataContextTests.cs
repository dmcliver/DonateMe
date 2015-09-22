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
        [Test]
        public void GeneratesDataOk()
        {
            DataContext context = new DataContext();

            BuildItemWithImage(context);

            context.SaveChanges();

            IList<ItemCategory> categories = context.Set<ItemCategory>().ToList();
            IList<Image> images = context.Set<Image>().ToList();

            Assert.That(categories, Is.Not.Null);
            Assert.True(images.Any());

            Uri uri = images.First().UriPath.GetUri();
            Assert.That(uri.ToString(), Is.EqualTo("/Users/dmcliver"));
        }

        private static void BuildItemWithImage(DataContext context)
        {
            var child = new ItemCategory(Guid.NewGuid(), "Instruments");
            var parent = new ItemCategory(Guid.NewGuid(), "Music");

            var itemCategoryRelation = new ItemCategoryRelation(parent, child);

            var item = new Item("Guitar", itemCategoryRelation);

            Image img = new Image(Guid.NewGuid(), new UriType("/Users/dmcliver"), item);

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
