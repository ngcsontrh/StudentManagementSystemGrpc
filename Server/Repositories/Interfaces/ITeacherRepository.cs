using Server.Entities;

namespace Server.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetAsync(int id);
        Task<List<Teacher>?> GetAllAsync();
    }
}
