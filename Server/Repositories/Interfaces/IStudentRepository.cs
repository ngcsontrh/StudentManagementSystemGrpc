using Server.Entities;
using Shared.Models;

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
        Task<List<Student>?> SearchAsync(SearchStudentModel studentSearch); // BUG
        Task<List<Student>?> GetByNameAsync(string name);
        Task<List<Student>?> GetByAddressAsync(string address);
        Task<List<Student>?> GetByClassAsync(int classId);
        Task<List<Student>?> GetByDateAsync(DateTime dateStart, DateTime dateEnd);
        Task<List<Student>?> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 10);
    }
}
