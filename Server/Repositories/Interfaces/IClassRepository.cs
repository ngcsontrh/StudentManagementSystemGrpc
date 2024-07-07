using Server.DTOs;
using Server.Entities;

namespace Server.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<List<Class>?> GetAllAsync();
        Task<Class?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        Task<List<ClassChartDTO>> GetClassChartAsync();
    }
}
