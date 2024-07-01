using Server.Entities;

namespace Server.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>?> GetAllAsync();
        Task<int> CountAsync();
        Task<Student?> GetAsync(int id);
        Task<Student?> GetDetailsAsync(int id);
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<List<Student>?> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 10);
    }
}
