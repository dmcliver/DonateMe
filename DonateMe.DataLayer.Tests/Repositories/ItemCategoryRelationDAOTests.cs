using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using NLog;
using NSubstitute;
using NUnit.Framework;
using TestCommon;
using TestCommon.Builders;

namespace DonateMe.DataLayer.Tests.Repositories
{
    public class ItemCategoryRelationDAOTests
    {
        private readonly ItemCategoryBuilder _itemCategoryBuilder = new ItemCategoryBuilder();

        private const string Id = "A3C7A21C-4C61-4A7A-B7D3-3B7D6EFA6F8A";
        private const string MusicVideoCategory = "Video";
        private const string MusicInstrumentCategory = "Instruments";
        private const string MusicCategory = "Music";

        private ItemCategoryCount _musicParent;
        private ItemCategoryCount _musicChild ;
        private ItemCategoryCount _musicChild2;
        private ItemCategoryCount _musicGrandChild;

        [SetUp]
        public void Init()
        {
            _musicParent = _itemCategoryBuilder.WithIdAndName(Id, MusicCategory).Build();
            _musicChild = _itemCategoryBuilder.WithIdAndName("057ED8FF-84D9-4263-8722-E2A963924B4B", MusicInstrumentCategory).Build();
            _musicChild2 = _itemCategoryBuilder.WithIdAndName("00F5CA2C-8717-4DBA-8A1A-8A687457B2A6", MusicVideoCategory).Build();
            _musicGrandChild = _itemCategoryBuilder.WithIdAndName("F9E6DBA6-4593-4484-B5CE-9F17266C2D5D", "Stringed").Build();
        }

        [Test]
        public void GetChildCategoriesByParentId_ReturnsOnlyAssociatedCategories()
        {
            IDbProxyContext context = Mock.Instantiate<IDbProxyContext>();

            List<ItemCategoryCount> categories = new List<ItemCategoryCount> { _musicParent, _musicChild, _musicChild2, _musicGrandChild };
            context.Set<ItemCategoryCount>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, Mock.Instantiate<ILogger>());
            IEnumerable<ItemCategoryCount> categoryCounts = itemCategoryRelationDAO.GetChildCategoriesByParentId(Guid.Parse(Id)).ToList();

            Assert.That(categoryCounts.Count(), Is.EqualTo(2));
            Assert.That(categoryCounts.Single(c => c.Name == MusicVideoCategory).TotalCount, Is.EqualTo(0));
            Assert.That(categoryCounts.Single(c => c.Name == MusicInstrumentCategory).TotalCount, Is.EqualTo(1));
        }

        [Test]
        public void GetTopLevelCategoriesWithNoChildren_ReturnsOnlyTopLevelCategoriesWithNoChildren()
        {
            const string expectedItemName = "Books";

            IDbProxyContext context = Mock.Instantiate<IDbProxyContext>();

            List<ItemCategoryCount> categories = new List<ItemCategoryCount>
            {
                _musicParent, _musicChild, _musicChild2, _musicGrandChild//, BuildItemCategory(expectedItemName)
            };
            context.Set<ItemCategoryCount>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, Mock.Instantiate<ILogger>());
            List<ItemCategoryCount> itemCategories = itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().ToList();
            
            Assert.That(itemCategories.Count, Is.EqualTo(1));
            Assert.That(itemCategories[0].Name, Is.EqualTo(expectedItemName));
        }

        [Test]
        public void GetTopLevelCategoriesWithChildren_ReturnsOnlyTopLevelCategoriesWithChildren()
        {
            IDbProxyContext context = Mock.Instantiate<IDbProxyContext>();

            List<ItemCategoryCount> categories = new List<ItemCategoryCount>
            {
                _musicParent, _musicChild, _musicChild2, _musicGrandChild//, BuildItemCategory("Books")
            };
            context.Set<ItemCategoryCount>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, Mock.Instantiate<ILogger>());
            List<ItemCategoryCount> itemCategories = itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().ToList();

            Assert.That(itemCategories.Count, Is.EqualTo(1));
            Assert.That(itemCategories[0].Name, Is.EqualTo(MusicCategory));
        }

        [Test]
        public void TestQuery()
        {
            ItemCategoryDAO dao = new ItemCategoryDAOImpl(new DbProxyContext(), Mock.Instantiate<ILogger>());
            dao.GetTopLevelCategoriesWithChildren();
        }
    }
}