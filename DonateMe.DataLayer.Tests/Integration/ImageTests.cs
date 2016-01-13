using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NUnit.Framework;

namespace DonateMe.DataLayer.Tests.Integration
{
    [TestFixture]
    public class ImageTests
    {
        private DbContextTransaction _transaction;
        const string Path = "/Users/dmcliver";

        [Test]
        public void GeneratesItemWithImageOk()
        {
            DbContextImpl context = new DbContextImpl();
            _transaction = context.Database.BeginTransaction();

            BuildItemWithImage(context);

            context.SaveChanges();

            IList<Image> images = context.Set<Image>().ToList();

            Assert.True(images.Any());

            UriType uriPath = images.First().UriPath;
            Uri uri = uriPath.GetUri();
            Assert.That(uri.ToString(), Is.EqualTo(Path));
            Assert.That(uriPath.Path, Is.EqualTo(Path));
        }

        [TearDown]
        public void End()
        {
            if(_transaction != null)
                _transaction.Rollback();
        }

        private static void BuildItemWithImage(DbContextImpl context)
        {
            var child = new ItemCategory(Guid.NewGuid(), "Instruments");
            var parent = new ItemCategory(Guid.NewGuid(), "Music");

            var itemCategoryRelation = new ItemCategory(Guid.NewGuid(), "Instruments");

            var item = new Item("Guitar", itemCategoryRelation);

            Image img = new Image(Guid.NewGuid(), new UriType(Path), item);

           // AddEntities(context, child, parent, itemCategoryRelation, item, img);
        }

        private static void AddEntities(
            DbContextImpl context, ItemCategory child, ItemCategory parent, Item item, Image img
        )
        {
            context.Set<ItemCategory>().Add(child);
            context.Set<ItemCategory>().Add(parent);
            context.Set<Item>().Add(item);
            context.Set<Image>().Add(img);
        }
    }
}
