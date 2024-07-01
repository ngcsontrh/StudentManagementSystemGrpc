using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using ISession = NHibernate.ISession;
using Server.Entities;

namespace Server
{
    public class NHibernateHelper
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateHelper(string connectionString)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<Teacher>()
                    .AddFromAssemblyOf<Class>()
                    .AddFromAssemblyOf<Student>())
                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
