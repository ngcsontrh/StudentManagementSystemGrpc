using NHibernate.Linq;
using Server;
using Server.Entities;
using Server.Repositories.Interfaces;
using ISession = NHibernate.ISession;

namespace Client.Blazor.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISession _session;

        public StudentRepository(ISession session)
        {
            _session = session;
        }

        public async Task<int> CountAsync()
        {
            int count = await _session.Query<Student>().CountAsync();

            return count;
        }

        public async Task CreateAsync(Student student)
        {
            using(var transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(student);
                await transaction.CommitAsync();
            }
        }

        public async Task DeleteAsync(Student student)
        {
            using(var transaction = _session.BeginTransaction())
            {
                await _session.DeleteAsync(student);
                await transaction.CommitAsync();
            }
        }

        public async Task<List<Student>?> GetAllAsync()
        {
            List<Student>? students = await _session.Query<Student>()
                .Fetch(s => s.StudentClass)
                .ToListAsync();

            return students;
        }

        public async Task<Student?> GetAsync(int id)
        {
            Student student = await _session.Query<Student>()
                .Fetch(s => s.StudentClass)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public async Task<Student?> GetDetailsAsync(int id)
        {
            Student student = await _session.Query<Student>()
                .Fetch(s => s.StudentClass)
                    .ThenFetch(c => c.ClassTeacher)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public async Task<List<Student>?> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 10)
        {
            int pageSkip = (pageNumber - 1) * pageSize;

            List<Student>? students = await _session.Query<Student>()
                .Fetch(s => s.StudentClass)
                .Skip(pageSkip)
                .Take(pageSize)
                .ToListAsync();

            return students;
        }

        public async Task UpdateAsync(Student student)
        {
            using (var transaction = _session.BeginTransaction())
            {
                await _session.UpdateAsync(student);
                transaction.Commit();
            }
        }
    }
}
