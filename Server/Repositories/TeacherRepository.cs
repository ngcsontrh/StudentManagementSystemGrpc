using ISession =  NHibernate.ISession;
using Server.Entities;
using Server.Repositories.Interfaces;
using NHibernate.Linq;

namespace Server.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ISession _session;

        public TeacherRepository(ISession session)
        {
            _session = session;
        }

        public async Task<List<Teacher>?> GetAllAsync()
        {
            List<Teacher>? teachers = await _session.Query<Teacher>().ToListAsync();

            return teachers;
        }

        public async Task<Teacher?> GetAsync(int id)
        {
            Teacher teacher = await _session.Query<Teacher>().FirstOrDefaultAsync(t => t.Id == id);

            return teacher;
        }
    }
}
