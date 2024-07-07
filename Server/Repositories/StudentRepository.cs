using NHibernate.Linq;
using Server;
using Server.DTOs;
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
                .ThenFetch(c => c.ClassTeacher)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            using (var transaction = _session.BeginTransaction())
            {
                await _session.UpdateAsync(student);
                transaction.Commit();
            }
        }

        public async Task<PageViewDTO<Student>> GetPaginationAsync(Server.DTOs.SearchStudentDTO searchStudent, int pageNumber = 1, int pageSize = 10)
        {
            int pageSkip = (pageNumber - 1) * pageSize;
            var query = _session.Query<Student>()
                            .Fetch(s => s.StudentClass)
                            .ThenFetch(c => c.ClassTeacher).AsQueryable();
            query = Filter(query, searchStudent);
            var result = new PageViewDTO<Student>
            {
                Total = await query.CountAsync(),
                Students = await query!.Skip(pageSkip).Take(pageSize).ToListAsync()
            };
            return result;
        }

        public async Task<List<StudentAgeChartDTO>> GetStudentAgesChartAsync(int classId = -1)
        {
            var query = _session.Query<Student>();
            if (classId != -1)
            {
                query = query.Where(s => s.StudentClass.Id == classId);
            }
            List<StudentAgeChartDTO> result = await query
                .GroupBy(s => DateTime.Now.Year - s.Birthday.Year)
                .Select(s => new StudentAgeChartDTO
                {
                    Age = s.Key,
                    NumberOfStudent = s.Count()
                })
                .OrderBy(r => r.Age)
                .ToListAsync();
            return result;
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
    }
}
