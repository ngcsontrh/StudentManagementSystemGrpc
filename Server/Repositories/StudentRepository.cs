using NHibernate.Linq;
using Server;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared.DTOs;
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
                .ThenFetch(c => c.ClassTeacher)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public async Task<List<Student>?> SearchAsync(SearchStudentDTO studentSearch)
        {
            var query = _session.Query<Student>().AsQueryable();
            query = Filter(query, studentSearch);
            var list = await query.ToListAsync();
            return list;
        }

        public async Task<List<Student>?> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 10)
        {
            int pageSkip = (pageNumber - 1) * pageSize;
            var students = await _session.Query<Student>()
                            .Fetch(s => s.StudentClass)
                            .ThenFetch(c => c.ClassTeacher)
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

        public async Task<List<Student>?> SearchWithPaginationAsync(SearchStudentDTO studentSearch, int pageNumber = 1, int pageSize = 10)
        {
            int pageSkip = (pageNumber - 1) * pageSize;
            var query = _session.Query<Student>()
                            .Fetch(s => s.StudentClass)
                            .ThenFetch(c => c.ClassTeacher).AsQueryable();
            query = Filter(query, studentSearch);
            var students = await query!.Skip(pageSkip).Take(pageSize).ToListAsync();
            return students;
        }

        private IQueryable<Student>? Filter(IQueryable<Student> query, SearchStudentDTO studentSearchField)
        {
            if (studentSearchField.Id.HasValue)
            {
                query = query.Where(student => student.Id == studentSearchField.Id.Value);
            }
            if (!string.IsNullOrWhiteSpace(studentSearchField.Name))
            {
                query = query.Where(s => s.FullName.Contains(studentSearchField.Name));
            }
            if (!string.IsNullOrEmpty(studentSearchField.Address))
            {
                query = query.Where(s => s.Address.Contains(studentSearchField.Address));
            }
            if (studentSearchField.StartDate.HasValue)
            {
                query = query.Where(student => student.Birthday.Date >= studentSearchField.StartDate.Value.Date);
            }
            if (studentSearchField.EndDate.HasValue)
            {
                query = query.Where(student => student.Birthday.Date <= studentSearchField.EndDate.Value.Date);
            }
            if (studentSearchField.ClassId.HasValue)
            {
                query = query.Where(student => student.StudentClass.Id == studentSearchField.ClassId.Value);
            }
            return query;
        }

        public async Task<int> CountWithSearch(SearchStudentDTO searchStudentDTO)
        {
            var query = _session.Query<Student>();
            query = Filter(query, searchStudentDTO);
            int count = await query.CountAsync();
            return count;
        }
    }
}
