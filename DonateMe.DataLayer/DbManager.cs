using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;

namespace DonateMe.DataLayer
{
    /// <summary>
    /// Database Manager to obtain an NHibernate unit of work session
    /// </summary>
    public class DbManager : IDbManager
    {
        private static ISessionFactory _factory;

        public ISession ObtainSession()
        {
            if (_factory == null || _factory.IsClosed)
            {
                var fluentConfiguration = Fluently.Configure();

                if (HttpContext.Current == null)
                    fluentConfiguration.CurrentSessionContext<ThreadStaticSessionContext>();
                else
                    fluentConfiguration.CurrentSessionContext<WebSessionContext>();

                _factory = fluentConfiguration.Database
                                              (
                                                  MsSqlConfiguration.MsSql2008
                                                                    .ShowSql()
                                                                    .ConnectionString(conn => conn.FromConnectionStringWithKey("DonateMeDb"))
                                              )
                                              .Mappings(mc => mc.FluentMappings.AddFromAssemblyOf<DbManager>())
                                              .BuildSessionFactory();
            }

            if (!CurrentSessionContext.HasBind(_factory))
            {
                ISession session = _factory.OpenSession();
                CurrentSessionContext.Bind(session);
                return session;
            }
            
            return _factory.GetCurrentSession();
        }

        public static void Close()
        {
            if (_factory != null && !_factory.IsClosed)
            {
                if (CurrentSessionContext.HasBind(_factory))
                {
                    _factory.GetCurrentSession().Close();
                }

                _factory.Close();
            }
        }
    }
}