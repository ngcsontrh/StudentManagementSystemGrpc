using ISession = NHibernate.ISession;
using Server.Entities;
using Server.Repositories.Interfaces;
using NHibernate.Linq;

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
    }
}
