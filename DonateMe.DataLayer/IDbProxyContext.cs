using System;
using System.Data.Entity;
using System.Linq;
using DonateMe.Common;

namespace DonateMe.DataLayer
{
    [Injected]
    public interface IDbProxyContext : IDisposable
    {
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose(bool disposing);
        T Add<T>(T entity) where T: class;
    }
}