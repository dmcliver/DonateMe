using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using idx = System.Data.Entity.Infrastructure.Annotations.IndexAnnotation;
using DonateMe.BusinessDomain.Entities;

namespace DonateMe.DataLayer.Mappings
{
    public class BrandMapping
    {
        public static void ConfigureBrand(DbModelBuilder modelBuilder)
        {
            var brandConfig = modelBuilder.Entity<Brand>();
            brandConfig.ToTable("Brand");

            brandConfig.HasKey(b => b.BrandId);
            brandConfig.Property(b => b.BrandId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            brandConfig.Property(b => b.Name).HasMaxLength(255)
                       .HasColumnAnnotation(idx.AnnotationName, new idx(new IndexAttribute("IDX_UNQ_Brand_Name"){IsUnique = true}))
                       .IsRequired();
        }
    }
}