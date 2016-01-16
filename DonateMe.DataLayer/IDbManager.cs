using DonateMe.Common;
using NHibernate;

namespace DonateMe.DataLayer
{
    /// <summary>
    /// Database manager to obtain an NHibernate unit of work session
    /// </summary>
    [Injected]
    public interface IDbManager
    {
        ISession ObtainSession();
    }
}