using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;
using DonateMe.DataLayer.Tests.Builders;
using NLog;
using NSubstitute;
using NUnit.Framework;
using TestCommon;

namespace DonateMe.DataLayer.Tests.Repositories
{
    public class ItemCategoryRelationDAOTests
    {
        private readonly ItemCategoryRelationBuilder _builder = new ItemCategoryRelationBuilder();
        private readonly ItemCategoryBuilder _itemCategoryBuilder = new ItemCategoryBuilder();

        private const string Id = "A3C7A21C-4C61-4A7A-B7D3-3B7D6EFA6F8A";
        private const string MusicVideoCategory = "Video";
        private const string MusicInstrumentCategory = "Instruments";

        private ItemCategory _musicParent;
        private ItemCategory _musicChild ;
        private ItemCategory _musicChild2;
        private ItemCategory _musicGrandChild;

        [SetUp]
        public void Init()
        {
            _musicParent = _itemCategoryBuilder.WithIdAndName(Id, "Music").Build();
            _musicChild = _itemCategoryBuilder.WithIdAndName("057ED8FF-84D9-4263-8722-E2A963924B4B", MusicInstrumentCategory).Build();
            _musicChild2 = _itemCategoryBuilder.WithIdAndName("00F5CA2C-8717-4DBA-8A1A-8A687457B2A6", MusicVideoCategory).Build();
            _musicGrandChild = _itemCategoryBuilder.WithIdAndName("F9E6DBA6-4593-4484-B5CE-9F17266C2D5D", "Stringed").Build();
        }

        [Test]
        public void GetChildCategoriesByParentId_ReturnsOnlyAssociatedCategories()
        {
            IDbProxyContext context = Mock.For<IDbProxyContext>();

            List<ItemCategory> categories = new List<ItemCategory> { _musicParent, _musicChild, _musicChild2, _musicGrandChild };
            context.Set<ItemCategory>().Returns(categories.AsQueryable());

            List<ItemCategoryRelation> itemCategoryRelations = BuildItemCategoryRelations(_musicParent, _musicChild, _musicChild2, _musicGrandChild);
            context.Set<ItemCategoryRelation>().Returns(itemCategoryRelations.AsQueryable());

            var itemCategoryRelationDAO = new ItemCategoryRelationDAOImpl(context, Mock.For<ILogger>());
            IEnumerable<ItemCategoryCount> categoryCounts = itemCategoryRelationDAO.GetChildCategoriesByParentId(Guid.Parse(Id)).ToList();

            Assert.That(categoryCounts.Count(), Is.EqualTo(2));
            Assert.That(categoryCounts.Single(c => c.Name == MusicVideoCategory).TotalCount, Is.EqualTo(0));
            Assert.That(categoryCounts.Single(c => c.Name == MusicInstrumentCategory).TotalCount, Is.EqualTo(1));
        }

        [Test]
        public void GetTopLevelCategoriesWithNoChildren_ReturnsOnlyTopLevelCategoriesWithNoChildren()
        {
            Assert.Fail();
        }

        [Test]
        public void GetTopLevelCategoriesWithChildren_ReturnsOnlyTopLevelCategoriesWithChildren()
        {
            Assert.Fail();
        }

        private List<ItemCategoryRelation> BuildItemCategoryRelations(ItemCategory musicParent, ItemCategory musicChild, ItemCategory musicChild2, ItemCategory musicGrandChild)
        {
            var itemCategoryRelations = new List<ItemCategoryRelation>
            {
                _builder.WithParentAndChild(musicParent, musicChild).Build(),
                _builder.WithParentAndChild(musicParent, musicChild2).Build(),
                _builder.WithParentAndChild(musicChild, musicGrandChild).Build()
            };
            return itemCategoryRelations;
        }
    }
}