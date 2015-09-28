﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Controllers;
using DonateMe.Web.Models;
using NSubstitute;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace DonateMe.Web.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void GetChildren_WithChildrenFound_BuildsItemNodeModel()
        {
            Guid parentId = Guid.NewGuid();
            var childCategories = new List<ItemCategory>{new ItemCategory(Guid.NewGuid(), "Menu")};
            var expectedModel = new List<ItemNodeModel>();

            var itemRepository = Mock.For<IItemRepository>();
            
            var itemNodeModelBuilder = Mock.For<IItemNodeModelBuilder>();
            itemNodeModelBuilder.Build(parentId, null, childCategories).Returns(expectedModel);

            var itemCategoryRelationRepository = Mock.For<IItemCategoryRelationRepository>();
            itemCategoryRelationRepository.GetChildCategoriesByParentId(parentId).Returns(childCategories);
            itemCategoryRelationRepository.GetTopLevelCategories().Returns((IEnumerable<ItemCategory>) null);

            var controller = new HomeController(itemCategoryRelationRepository, itemNodeModelBuilder, itemRepository);
            var model = (controller.GetChildren(parentId) as ViewResult).Model as ItemCategoryModelContainer;

            itemNodeModelBuilder.Received(1).Build(parentId, null, childCategories);
            Assert.That(model.Categories, Is.SameAs(expectedModel));
        }

        [Test]
        public void GetChildren_WithNoChildrenFound_DoesntBuildItemNodeModel()
        {
            Guid parentId = Guid.NewGuid();
            const string expectedNameOfFirstModel = "Parent";

            var itemNodeModelBuilder = Mock.For<IItemNodeModelBuilder>();
            var itemRepository = Mock.For<IItemRepository>();
            itemRepository.GetByCategoryId(parentId).Returns(new List<Item>{new Item("Guitar", new ItemCategoryRelation(new ItemCategory(Guid.NewGuid(), "?"), new ItemCategory(Guid.NewGuid(), "?")))});

            var itemCategoryRelationRepository = Mock.For<IItemCategoryRelationRepository>();
            itemCategoryRelationRepository.GetChildCategoriesByParentId(parentId).Returns(new List<ItemCategory>());
            itemCategoryRelationRepository.GetTopLevelCategories().Returns(new List<ItemCategory>{new ItemCategory(Guid.NewGuid(), expectedNameOfFirstModel)});

            var controller = new HomeController(itemCategoryRelationRepository, itemNodeModelBuilder, itemRepository);
            var model = (controller.GetChildren(parentId) as ViewResult).Model as ItemCategoryModelContainer;

            itemNodeModelBuilder.DidNotReceiveWithAnyArgs().Build(Guid.Empty, null, null);
            itemRepository.Received(1).GetByCategoryId(parentId);

            Assert.That(model.Categories.Count(), Is.EqualTo(1));
            Assert.That(model.Categories.ElementAt(0).Name, Is.EqualTo(expectedNameOfFirstModel));
        }
    }
}
