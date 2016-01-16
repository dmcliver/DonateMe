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
// ReSharper disable InconsistentNaming

namespace DonateMe.DataLayer.Tests.Repositories
{
    public class ItemCategoryRelationDAOTests
    {
        private ItemCategoryBuilder _itemCategoryBuilder;
        private readonly DbManager dbManager = new DbManager();

        private const string Id = "A3C7A21C-4C61-4A7A-B7D3-3B7D6EFA6F8A";
        private const string MusicVideoCategory = "Video";
        private const string MusicInstrumentCategory = "Instruments";
        private const string MusicCategory = "Music";

        private ItemCategory _musicParent;
        private ItemCategory _musicChild ;
        private ItemCategory _musicChild2;
        private ItemCategory _musicGrandChild;

        [SetUp]
        public void Init()
        {
            _itemCategoryBuilder = new ItemCategoryBuilder();
            _musicParent = _itemCategoryBuilder.WithIdAndName(Id, MusicCategory).Build();
            _musicChild = _itemCategoryBuilder.With(_musicParent).WithIdAndName("057ED8FF-84D9-4263-8722-E2A963924B4B", MusicInstrumentCategory).Build();
            _musicChild2 = _itemCategoryBuilder.With(_musicParent).WithIdAndName("00F5CA2C-8717-4DBA-8A1A-8A687457B2A6", MusicVideoCategory).Build();
            _musicGrandChild = _itemCategoryBuilder.With(_musicChild).WithIdAndName("F9E6DBA6-4593-4484-B5CE-9F17266C2D5D", "Stringed").Build();
        }

        [Test]
        public void GetChildCategoriesByParentId_ReturnsOnlyAssociatedCategories()
        {
            IDbProxyContext context = Mock.It<IDbProxyContext>();

            List<ItemCategory> categories = new List<ItemCategory> { _musicParent, _musicChild, _musicChild2, _musicGrandChild };
            context.Set<ItemCategory>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, Mock.It<IDbManager>(), Mock.It<ILogger>());
            IEnumerable<ItemCategoryCount> categoryCounts = itemCategoryRelationDAO.GetChildCategoriesByParentId(Guid.Parse(Id)).ToList();

            Assert.That(categoryCounts.Count(), Is.EqualTo(2));
            Assert.That(categoryCounts.Count(c => c.Name == MusicVideoCategory), Is.EqualTo(1));
            Assert.That(categoryCounts.Count(c => c.Name == MusicInstrumentCategory), Is.EqualTo(1));
        }

        [Test]
        public void GetTopLevelCategoriesWithNoChildren_ReturnsOnlyTopLevelCategoriesWithNoChildren()
        {
            const string expectedItemName = "Books";

            IDbProxyContext context = Mock.It<IDbProxyContext>();

            List<ItemCategory> categories = new List<ItemCategory>
            {
                _musicParent, _musicChild, _musicChild2, _musicGrandChild, new ItemCategory(Guid.NewGuid(), expectedItemName)
            };
            context.Set<ItemCategory>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, dbManager, Mock.It<ILogger>());
            List<ItemCategoryCount> itemCategories = itemCategoryRelationDAO.GetTopLevelCategoriesWithNoChildren().ToList();
            
            Assert.That(itemCategories.Count, Is.EqualTo(2));
            Assert.That(itemCategories[0].Name, Is.EqualTo(expectedItemName));
        }

        [Test]
        public void GetTopLevelCategoriesWithChildren_ReturnsOnlyTopLevelCategoriesWithChildren()
        {
            IDbProxyContext context = Mock.It<IDbProxyContext>();

            List<ItemCategory> categories = new List<ItemCategory>
            {
                _musicParent, _musicChild, _musicChild2, _musicGrandChild//, BuildItemCategory("Books")
            };
            context.Set<ItemCategory>().Returns(categories.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryDAOImpl(context, dbManager, Mock.It<ILogger>());
            List<ItemCategoryCount> itemCategories = itemCategoryRelationDAO.GetTopLevelCategoriesWithChildren().ToList();

            Assert.That(itemCategories.Count, Is.EqualTo(1));
            Assert.That(itemCategories[0].Name, Is.EqualTo(MusicCategory));
            Assert.That(itemCategories[0].TotalCount, Is.EqualTo(2));
        }

        [Test, Ignore]
        public void TestQuery()
        {
            ItemCategoryDAO dao = new ItemCategoryDAOImpl(new DbProxyContext(), Mock.It<IDbManager>(), Mock.It<ILogger>());

            IEnumerable<ItemCategoryCount> categories = dao.GetChildCategoriesByParentId(Guid.Parse(Id));

            Assert.True(categories.Any());
        }

        [TestFixtureTearDown]
        public void End()
        {
            DbManager.Close();
        }
    }
}
