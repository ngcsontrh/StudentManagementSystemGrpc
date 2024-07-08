using ConsoleAppNHibernate;
using QLSVConsole.Entities;

namespace QLSVConsole.Services.ClassService
{
    internal class ClassService : IClassService
    {
        public bool Any(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Query<Class>().Any(s => s.Id == id);
                }
            }
        }

        public Class? Get(int classId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Get<Class>(classId);
                }
            }
        }

        public List<Class> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Query<Class>().ToList();
                }
            }
        }
    }
}
