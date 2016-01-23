using System.Collections.Generic;
using System.Linq;

namespace DonateMe.BusinessDomain
{
    public class CategoryHierarchyServiceImpl : CategoryHierarchyService
    {
        public CategoryHierarchyServiceImpl()
        {
            Separator = " - ";
        }

        public string Separator { set; private get; }

        public IEnumerable<string> GroupCategoryNamesAsFlatListHierarchy(IEnumerable<IGrouping<string, ParentChildCategory>> grouping)
        {
            var hierarchicalCategories = new List<string>();

            List<IGrouping<string, ParentChildCategory>> list = grouping.ToList();
            list.ForEach(g => Compute(g.Key, g.Key, list, hierarchicalCategories));

            return hierarchicalCategories;
        }

        private void Compute(string expression, string key, ICollection<IGrouping<string, ParentChildCategory>> grouping, ICollection<string> categoryList)
        {
            if (grouping.All(g => g.Key != key) && expression.StartsWith(Separator))
                categoryList.Add(expression.TrimStart(Separator.ToArray()));
            else if(grouping.Any(g => g.Key == key))
                grouping.Single(g => g.Key == key).ToList().ForEach(c => Compute(expression + Separator + c.Child, c.Child, grouping, categoryList));
        }
    }
}

