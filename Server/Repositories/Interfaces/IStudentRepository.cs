using Server.Entities;
using Shared.DTOs;

namespace Server.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>?> GetAllAsync();
        Task<int> CountAsync();
        Task<Student?> GetAsync(int id);
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<List<Student>?> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 10);
        Task<List<Student>?> SearchAsync(SearchStudentDTO studentSearch);
    }
}
