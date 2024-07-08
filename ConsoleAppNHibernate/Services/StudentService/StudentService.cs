using ConsoleAppNHibernate;
using NHibernate;
using NHibernate.Linq;
using QLSVConsole.Entities;

namespace QLSVConsole.Services.StudentService
{
    internal class StudentService : IStudentService
    {
        public void Add(Student student)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(student);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int studentId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var student = session.Get<Student>(studentId);
                    session.Delete(student);
                    transaction.Commit();
                }
            }
        }

        public Student? Get(int studentId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Query<Student>()
                        .Fetch(s => s.Class)
                            .ThenFetch(c => c.Teacher)
                        .FirstOrDefault(s => s.Id == studentId);
                }
            }
        }

        public List<Student> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Query<Student>()
                        .Fetch(s => s.Class)
                            .ThenFetch(c => c.Teacher)
                        .ToList();
                }
            }
        }

        public void Update(Student student)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(student);
                    transaction.Commit();
                }
            }
        }

        public bool Any(int studentId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return session.Query<Student>().Any(s => s.Id == studentId);
                }
            }
        }
    }
}
