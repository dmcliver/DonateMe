using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonateMe.BusinessDomain.Entities
{
    [Table("Item")]
    public class Item
    {
        private Brand _brand;

        public Item(string name, ItemCategory parentItemCategory)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (parentItemCategory == null) throw new ArgumentNullException("parentItemCategory");

            Name = name;
            ParentItemCategory = parentItemCategory;
            ParentId = parentItemCategory.ItemCategoryId;
        }

        protected Item() {}

        [Key]
        public int ItemId { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Column]
        public string Description { get; set; }
        
        [Column]
        public string Model { get; set; }

        public int? BrandId { get; private set; }

        [ForeignKey("BrandId")]
        public Brand Brand
        {
            get { return _brand; }
            set
            {
                if (value != null)
                {
                    BrandId = value.BrandId;
                    _brand = value;
                }
            }
        }

        public Guid ParentId { get; set; }

        [ForeignKey("ParentId")]
        public ItemCategory ParentItemCategory { get; private set; }
    }
}
