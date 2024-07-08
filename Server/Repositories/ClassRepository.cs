using ISession = NHibernate.ISession;
using Server.Entities;
using Server.Repositories.Interfaces;
using NHibernate.Linq;
using Server.DTOs;

namespace Server.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ISession _session;

        public ClassRepository(ISession session)
        {
            _session = session;
        }

        public async Task<List<Class>?> GetAllAsync()
        {
            List<Class>? classes = await _session.Query<Class>().ToListAsync();

            return classes;
        }

        public async Task<Class?> GetAsync(int id)
        {
            Class clazz = await _session.Query<Class>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return clazz;
        }

        public async Task<bool> AnyAsync(int id)
        {
            bool exists = await _session.Query<Class>().AnyAsync(c => c.Id == id);
            return exists;
        }

        public async Task<List<ClassStudentCountDTO>> GetClassChartAsync()
        {
            List<Class> classes = await _session.Query<Class>().ToListAsync();
            int studentsCount = await _session.Query<Student>().CountAsync();
            var query = from c in classes
                        join s in _session.Query<Student>() on c.Id equals s.StudentClass.Id
                        group s by c into g
                        select new ClassStudentCountDTO
                        {
                            ClassName = g.Key.Name,
                            StudentPercentage = Math.Round((g.Count() / (double)studentsCount ) * 100, 2),
                        };
            List<ClassStudentCountDTO> chartData = query.ToList();

            return chartData;
        }
    }
}
