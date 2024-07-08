using Server.DTOs;
using Server.Entities;

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
        Task<PageViewDTO<Student>> GetPaginationAsync(SearchStudentDTO searchStudent, int pagenNumber = 1, int pageSize = 10);
        Task<List<StudentAgeDTO>> GetStudentAgesChartAsync(int classId = -1);
    }
}
