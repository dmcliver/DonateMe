using System;
using System.Data.Entity;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.DataLayer.Mappings;

namespace DonateMe.DataLayer
{
    public class DbContextImpl : DbContext
    {
        public DbContextImpl() : base("DonateMeDb")
        {
            Database.SetInitializer(new NullDatabaseInitializer<DbContextImpl>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ItemCategoryMapping.ConfigureCategory(modelBuilder);
            ItemCategoryRelationMapping.ConfigureCategoryRelation(modelBuilder);
            ItemMapping.ConfigureItem(modelBuilder);
            ImageMapping.ConfigureImage(modelBuilder);
            BrandMapping.ConfigureBrand(modelBuilder);
            UserProfileMappping.ConfigureProfile(modelBuilder);

            modelBuilder.ComplexType<UriType>();
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class DbProxyContext : IDbProxyContext
    {
        private readonly DbContextImpl _dbContext;

        public DbProxyContext(DbContextImpl dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        public DbProxyContext():this(new DbContextImpl()){}

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public T Add<T>(T entity) where T: class
        {
            return _dbContext.Set<T>().Add(entity);
        }

        public void Dispose(bool disposing)
        {
            _dbContext.Dispose(disposing);
        }
    }
}
