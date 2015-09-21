using System;
using System.Data.Entity;
using DonateMe.Common;

namespace DonateMe.DataLayer
{
    [Injected]
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose(bool disposing);
    }
}