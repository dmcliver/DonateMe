using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Web.Controllers;
using DonateMe.Web.Models;
using NUnit.Framework;

namespace DonateMe.Web.Tests
{
    [TestFixture]
    public class ItemNodeModelBuilderTest
    {
        [Test]
        public void Build_WithEmptyTopLevelCategories_ReturnsEmptyModels()
        {
            ItemNodeModelBuilder builder = new ItemNodeModelBuilder();
            
            List<ItemNodeModel> models = builder.Build(Guid.NewGuid(), new List<ItemCategory>(), new List<ItemCategory>{new ItemCategory(Guid.NewGuid(), "Child")});

            Assert.That(models, Is.Empty);
        }

        [Test]
        public void Build_WithTopLevelCategoriesThatDoesntRelateToParentId_ReturnsModelListWithNoChildren()
        {
            const string topLevelCategoryName = "Parent";
            
            ItemNodeModelBuilder builder = new ItemNodeModelBuilder();

            Guid parentId = Guid.NewGuid();
            List<ItemNodeModel> models = builder.Build(parentId, new List<ItemCategory>{new ItemCategory(Guid.NewGuid(), topLevelCategoryName)}, new List<ItemCategory> { new ItemCategory(Guid.NewGuid(), "Child") });

            Assert.That(models.Count, Is.EqualTo(1));
            Assert.That(models[0].Children, Is.Empty);
            Assert.That(models[0].Name, Is.EqualTo(topLevelCategoryName));
        }

        [Test]
        public void Build_WithTopLevelCategoriesThatRelatesToParentId_ReturnsModelListWithChildren()
        {
            const string topLevelCategoryName = "Parent";
            const string childCategoryName = "Child";

            ItemNodeModelBuilder builder = new ItemNodeModelBuilder();

            Guid parentId = Guid.NewGuid();
            List<ItemNodeModel> models = builder.Build(parentId, new List<ItemCategory> { new ItemCategory(parentId, topLevelCategoryName) }, new List<ItemCategory> { new ItemCategory(Guid.NewGuid(), childCategoryName) });

            Assert.That(models.Count, Is.EqualTo(1));
            Assert.That(models[0].Children.Count(), Is.EqualTo(1));
            Assert.That(models[0].Name, Is.EqualTo(topLevelCategoryName));
            Assert.That(models[0].Children.ElementAt(0).Name, Is.EqualTo(childCategoryName));
        }
    }
}
