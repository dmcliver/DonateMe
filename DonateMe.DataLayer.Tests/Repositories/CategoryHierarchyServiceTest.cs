using System;
using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NUnit.Framework;

namespace DonateMe.DataLayer.Tests.Repositories
{
    [TestFixture]
    public class CategoryHierarchyServiceTest
    {
        [Test]
        public void GroupCategoryNamesAsFlatListHierarch_SortsCategoriesIntoAHierarchyOk()
        {
            var musicParentCategory = new ItemCategory(Guid.Empty, "Music");
            var sportParentCategory = new ItemCategory(Guid.Empty, "Sport");
            var metalParentCategory = new ItemCategory(Guid.Empty, "Metal", musicParentCategory);
            var cricketParentCategory = new ItemCategory(Guid.Empty, "Cricket", sportParentCategory);

            IEnumerable<ItemCategory> itemCategories = BuildItemCategories(musicParentCategory, sportParentCategory, metalParentCategory, cricketParentCategory);

            CategoryHierarchyService service = new CategoryHierarchyServiceImpl{ Separator = " > "};
            IList<string> categories = service.GroupCategoryNamesAsFlatListHierarchy(BuildParentChildGrouping(itemCategories)).ToList();

            Assert.That(categories, Is.Not.Empty);
            Assert.That(categories.Count(), Is.EqualTo(10));

            Assert.That(categories.Contains("Music > Rap"));
            Assert.That(categories.Contains("Music > Metal > Metallica"));
            Assert.That(categories.Contains("Sport > Cricket > Wickets"));

            Assert.That(categories.Distinct().Count(), Is.EqualTo(10)); // Assert all entries are unique

            Assert.That(categories.Count(s => s.Contains("Cricket")), Is.EqualTo(3));
            Assert.That(categories.Count(s => s.Contains("Metal")), Is.EqualTo(2));
        }

        private static IEnumerable<IGrouping<string, ParentChildCategory>> BuildParentChildGrouping(IEnumerable<ItemCategory> itemCategories)
        {
            return itemCategories.Select(ic => new ParentChildCategory(ic.Name, ic.ParentItemCategory))
                                 .GroupBy(pcc => pcc.Parent)
                                 .ToList();
        }

        private static IEnumerable<ItemCategory> BuildItemCategories(ItemCategory musicParentCategory, ItemCategory sportParentCategory, ItemCategory metalParentCategory, ItemCategory cricketParentCategory)
        {
            IEnumerable<ItemCategory> itemCategories = new[]
            {
                new ItemCategory(Guid.Empty, "Gen"), 
                musicParentCategory,
                new ItemCategory(Guid.Empty, "Rap", musicParentCategory),
                new ItemCategory(Guid.Empty, "Opera", musicParentCategory),
                metalParentCategory,
                new ItemCategory(Guid.Empty, "Pantera", metalParentCategory),
                new ItemCategory(Guid.Empty, "Metallica", metalParentCategory),
                sportParentCategory,
                new ItemCategory(Guid.Empty, "Soccer", sportParentCategory),
                new ItemCategory(Guid.Empty, "Hockey", sportParentCategory),
                cricketParentCategory,
                new ItemCategory(Guid.Empty, "Bat", cricketParentCategory),
                new ItemCategory(Guid.Empty, "Ball", cricketParentCategory),
                new ItemCategory(Guid.Empty, "Wickets", cricketParentCategory),
            };

            return itemCategories;
        }
    }
}
