using ConsoleAppNHibernate.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNHibernate
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        static NHibernateHelper()
        {
            string connectionString = "Data Source=.;Initial Catalog=qlsv;Integrated Security=True;TrustServerCertificate=True";

            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<TeacherEntityMap>()
                    .AddFromAssemblyOf<ClassEntityMap>()
                    .AddFromAssemblyOf<StudentEntityMap>())
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
