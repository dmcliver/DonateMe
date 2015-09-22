using System.Data.Entity;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public class ImageMapping
    {
        public static void ConfigureImage(DbModelBuilder modelBuilder)
        {
            var imageConfig = modelBuilder.Entity<Image>();
            imageConfig.ToTable("Image");
            imageConfig.HasKey(i => i.ImageId);
            imageConfig.Property(i => i.UriPath.Path).HasColumnName("FilePath").IsRequired();
            imageConfig.HasRequired(i => i.Item).WithMany().HasForeignKey(i => i.ItemId);
        }
    }
}
