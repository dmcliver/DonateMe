using System;
using System.Data.Entity;
using System.Linq;
using DonateMe.Common;

namespace DonateMe.DataLayer
{
    [Injected]
    public interface IDbProxyContext : IDisposable
    {
        /// <summary>
        /// Returns the entity set in this context as an IQueryable implementation.
        /// </summary>
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        
        int SaveChanges();
        
        void Dispose(bool disposing);

        T Add<T>(T entity) where T: class;

        /// <summary>
        /// Updates the specified entity from a different context by adding it to this context and updating its entity state status to modified.
        /// </summary>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Updates the status of the specified entity to the desired state i.e added, modified, deleted or unchanged.
        /// </summary>
        void UpdateStatus<T>(T someEntity, EntityState state) where T : class;
    }
}