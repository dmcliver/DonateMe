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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ItemId { get; protected set; }

        [Required]
        public virtual string Name { get; protected set; }

        [Column]
        public virtual string Description { get; set; }
        
        [Column]
        public virtual string Model { get; set; }

        public virtual int? BrandId { get; protected set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand
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

        public virtual Guid ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual ItemCategory ParentItemCategory { get; protected set; }
    }
}
