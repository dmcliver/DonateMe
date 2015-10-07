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
            Database.SetInitializer(new DbInitializer());
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

    // ReSharper disable once UnusedMember.Global - Is used by Autofac IoC dependency injection container
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

        /// <summary>
        /// Returns the entity set in this context as an IQueryable implementation.
        /// </summary>
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

        /// <summary>
        /// Updates the specified entity from a different context by adding it to this context and updating its entity state status to modified.
        /// </summary>
        public void Update<T>(T entity) where T : class
        {
            Add(entity);
            UpdateStatus(entity, EntityState.Modified);
        }

        /// <summary>
        /// Updates the status of the specified entity to the desired state i.e added, modified, deleted or unchanged.
        /// </summary>
        public void UpdateStatus<T>(T someEntity, EntityState state) where T : class
        {
            _dbContext.Entry(someEntity).State = state;
        }

        public void Dispose(bool disposing)
        {
            _dbContext.Dispose(disposing);
        }
    }
}
