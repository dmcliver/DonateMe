using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.DataLayer.Repositories;
using DonateMe.Web.Controllers;
using DonateMe.Web.Models;
using NSubstitute;
using NUnit.Framework;
using TestCommon;
using TestCommon.Builders;
// ReSharper disable InconsistentNaming

namespace DonateMe.Web.Tests.Controllers
{
    [TestFixture]
    public class NavigatorControllerTests
    {
        readonly ItemCategoryBuilder _itemCategoryBuilder = new ItemCategoryBuilder();

        [Test]
        public void Get_WithNoCategoryIdAndTopLevelHavingNoChildren_ReturnsChildlessTopLevelCategoriesOnly()
        {
            var itemCategoryRelationDAO = Mock.It<ItemCategoryDAO>();
            itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().Returns(new List<ItemCategoryCount>());
            itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().Returns(new List<ItemCategoryCount>{_itemCategoryBuilder.BuildCount()});

            var controller = new NavigatorController(itemCategoryRelationDAO);
            IEnumerable<ItemNodeModel> itemNodeModels = controller.Get("null");
            
            Assert.That(itemNodeModels.Count(), Is.EqualTo(1));
            itemCategoryRelationDAO.DidNotReceiveWithAnyArgs().GetChildCategoriesByParentId(Guid.Empty);
        }

        [Test]
        public void Get_WithNoCategoryIdAndTopLevelHavingChildren_ReturnsParentTopLevelCategoriesOnly()
        {
            var itemCategoryRelationDAO = Mock.It<ItemCategoryDAO>();
            itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().Returns(new List<ItemCategoryCount> { new ItemCategoryBuilder().BuildCount() });
            itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().Returns(new List<ItemCategoryCount>());

            var controller = new NavigatorController(itemCategoryRelationDAO);
            IEnumerable<ItemNodeModel> itemNodeModels = controller.Get("null");

            Assert.That(itemNodeModels.Count(), Is.EqualTo(1));
            itemCategoryRelationDAO.DidNotReceiveWithAnyArgs().GetChildCategoriesByParentId(Guid.Empty);
        }

        [Test]
        public void Get_WithNoCategoryIdAndNoTopLevelCategories_ReturnsEmptyList()
        {
            var itemCategoryRelationDAO = Mock.It<ItemCategoryDAO>();
            itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().Returns(new List<ItemCategoryCount>());
            itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().Returns(new List<ItemCategoryCount>());

            NavigatorController controller = new NavigatorController(itemCategoryRelationDAO);
            IEnumerable<ItemNodeModel> itemNodeModels = controller.Get(null);

            Assert.That(itemNodeModels, Is.Empty);
            itemCategoryRelationDAO.DidNotReceiveWithAnyArgs().GetChildCategoriesByParentId(Guid.Empty);
        }

        [Test]
        public void Get_WithNoCategoryIdAndBothChildAndChildlessCategories_ReturnsAllTopLevelCategories()
        {
            const string expectedSecondItemName = "MyItemCategory2";

            var itemCategoryRelationDAO = Mock.It<ItemCategoryDAO>();
            itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().Returns(new List<ItemCategoryCount> { _itemCategoryBuilder.BuildCount() });
            itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().Returns(new List<ItemCategoryCount> { _itemCategoryBuilder.With(expectedSecondItemName).BuildCount() });

            var controller = new NavigatorController(itemCategoryRelationDAO);
            IEnumerable<ItemNodeModel> itemNodeModels = controller.Get("null").ToList();

            Assert.That(itemNodeModels.Count(), Is.EqualTo(2));
            Assert.That(itemNodeModels.Any(m => m.text == "MyItemCategory"));
            Assert.That(itemNodeModels.Any(m => m.text == expectedSecondItemName));
            itemCategoryRelationDAO.DidNotReceiveWithAnyArgs().GetChildCategoriesByParentId(Guid.Empty);
        }

        [Test]
        public void Get_WithCategoryId_ReturnsChildCategoriesOnly()
        {
            var id = Guid.NewGuid();
            const string expectedModelName = "Houses";

            var itemCategoryRelationDAO = Mock.It<ItemCategoryDAO>();
            itemCategoryRelationDAO.GetChildCategoriesByParentId(id).Returns(new List<ItemCategoryCount>{new ItemCategoryCount(expectedModelName, Guid.NewGuid(), 1)});

            var controller = new NavigatorController(itemCategoryRelationDAO);
            IEnumerable<ItemNodeModel> itemNodeModels = controller.Get(id.ToString()).ToList();

            itemCategoryRelationDAO.DidNotReceive().GetTopLevelCategoriesWithChildren();
            itemCategoryRelationDAO.DidNotReceive().GetTopLevelCategoriesWithNoChildren();

            Assert.That(itemNodeModels.Count(), Is.EqualTo(1));
            Assert.That(itemNodeModels.ElementAt(0).text, Is.EqualTo(expectedModelName));
        }
    }
}
