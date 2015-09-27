using System;
using System.Collections.Generic;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace DonateMe.Web.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void GetChildren_WithChildrenFound_BuildsItemNodeModel()
        {
            Guid parentId = Guid.NewGuid();
            var itemCategories = new List<ItemCategory>{new ItemCategory(Guid.NewGuid(), "Menu")};

            IItemNodeModelBuilder itemNodeModelBuilder = Substitute.For<IItemNodeModelBuilder>();
            IItemCategoryRelationRepository itemCategoryRelationRepository = Substitute.For<IItemCategoryRelationRepository>();
            itemCategoryRelationRepository.GetChildCategoriesByParentId(parentId).Returns(itemCategories);
            itemCategoryRelationRepository.GetTopLevelCategories().Returns((IEnumerable<ItemCategory>) null);

            HomeController controller = new HomeController(itemCategoryRelationRepository, itemNodeModelBuilder);
            controller.GetChildren(parentId);

            itemNodeModelBuilder.Received(1).Build(parentId, null, itemCategories);
        }

        [Test]
        public void GetChildren_WithNoChildrenFound_DoesntBuildItemNodeModel()
        {
            Guid parentId = Guid.NewGuid();

            IItemNodeModelBuilder itemNodeModelBuilder = Substitute.For<IItemNodeModelBuilder>();
            IItemCategoryRelationRepository itemCategoryRelationRepository = Substitute.For<IItemCategoryRelationRepository>();
            itemCategoryRelationRepository.GetChildCategoriesByParentId(parentId).Returns(new List<ItemCategory>());

            HomeController controller = new HomeController(itemCategoryRelationRepository, itemNodeModelBuilder);
            controller.GetChildren(parentId);

            itemNodeModelBuilder.DidNotReceiveWithAnyArgs().Build(Guid.Empty, null, null);
        }
    }
}
