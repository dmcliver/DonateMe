using DonateMe.BusinessDomain.Entities;

namespace DonateMe.BusinessDomain
{
    public class ParentChildCategory
    {
        private readonly string _child;
        private readonly string _parent;

        public ParentChildCategory(string child, ItemCategory parent)
        {
            _child = child;
            if (parent != null)
                _parent = parent.Name;
        }

        public string Child
        {
            get { return _child; }
        }

        public string Parent
        {
            get { return _parent; }
        }
    }
}