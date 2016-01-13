using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonateMe.BusinessDomain.Entities
{
    [Table("ItemCategory")]
    public class ItemCategory
    {
        private ItemCategory _parentItemCategory;

        protected ItemCategory()
        {
        }

        public ItemCategory(Guid itemCategoryId, string name)
        {
            if (itemCategoryId == null) 
                throw new ArgumentNullException("itemCategoryId");

            if (name == null) 
                throw new ArgumentNullException("name");

            ItemCategoryId = itemCategoryId;
            Name = name;
        }

        public ItemCategory(Guid itemCategoryId, string name, ItemCategory parentItemCategory) : this(itemCategoryId, name)
        {
            if (parentItemCategory == null) throw new ArgumentNullException("parentItemCategory");

            ParentItemCategory = parentItemCategory;
        }

        [Key]
        public Guid ItemCategoryId { get; private set; }

        [Required]
        public string Name { get; private set; }

        public Guid? ParentItemCategoryId { get; set; }

        [ForeignKey("ParentItemCategoryId")]
        public ItemCategory ParentItemCategory
        {
            get { return _parentItemCategory; }
            set
            {
                if (value != null)
                {
                    _parentItemCategory = value;
                    ParentItemCategoryId = value.ItemCategoryId;
                }
            }
        }
    }
}
